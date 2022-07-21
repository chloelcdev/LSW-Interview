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
        if (Input.GetKeyDown(KeyCode.Alpha0))
            SetAnimationState(PlayerAnimState.Idle);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetAnimationState(PlayerAnimState.Walking);

        if (Input.GetKeyDown(KeyCode.Alpha2))
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