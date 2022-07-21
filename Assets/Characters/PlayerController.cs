using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerAnimState _currentAnimationState = PlayerAnimState.Idle;
    Animator _animator;
    Rigidbody2D _rb;

    Vector2 _velocity;

    [SerializeField] float dragLerp = 0.03f;

    [SerializeField] float speed = 1;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            _velocity += input * speed * Time.deltaTime;
            SetAnimationState(PlayerAnimState.Walking);
        }

        ApplyDrag();
        ApplyVelocity();
        ApplyDirection();
    }

    void ApplyDirection()
    {
        // flipX doesn't work with the animations, we're going to use scaling instead

        if (_velocity.x < -0.01)
        {
            transform.localScale = new Vector2(-1,1);
        }
        else if (_velocity.x > 0.01)
        {
            transform.localScale = Vector2.one;
        }
    }

    void ApplyDrag()
    {
        _velocity = Vector2.Lerp(_velocity, Vector2.zero, dragLerp);
    }

    void ApplyVelocity()
    {
        // we're using moveposition because we don't want the physics engine doing much with our character.
        _rb.MovePosition(_rb.position + _velocity);
    }

    void DoInteract()
    {
        _animator.SetTrigger("Interact");
    }

    void SetAnimationState(PlayerAnimState state)
    {
        _currentAnimationState = state;
        _animator.SetInteger("AnimationState", (int)_currentAnimationState);
    }
}

// Explicitly setting the values here to keep this from getting messed up in serialization if it's changed in weird ways later
enum PlayerAnimState
{
    Idle = 0,
    Walking = 1,
}