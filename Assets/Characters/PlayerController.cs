using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerAnimState _currentAnimationState = PlayerAnimState.Idle;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector2 movementInput = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
        HandleMovement(movementInput);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            DoInteract();
    }

    void HandleMovement(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            SetAnimationState(PlayerAnimState.Idle);
            return;
        }

        SetAnimationState(PlayerAnimState.Walking);
    }

    void DoInteract()
    {
        animator.SetTrigger("Interact");
    }

    void SetAnimationState(PlayerAnimState state)
    {
        _currentAnimationState = state;
        animator.SetInteger("AnimationState", (int)_currentAnimationState);
    }
}

// Explicitly setting the values here to keep this from getting messed up in serialization if it's changed in weird ways later
enum PlayerAnimState
{
    Idle = 0,
    Walking = 1,
}