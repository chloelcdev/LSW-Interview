using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    public static PlayerController localPlayer;

    [SerializeField] CharacterBarController infoBar;

    [SerializeField] SpriteRenderer headSprite;

    [SerializeField] PlayerStatsAndEquipsScrob inventory;

    PlayerAnimState _currentAnimationState = PlayerAnimState.Idle;
    Animator _animator;
    Rigidbody2D _rb;

    public InteractionNotifier _interactionNotifier;

    Vector2 _velocity;

    [SerializeField] float dragLerp = 0.03f;

    [SerializeField] float speed = 1;

    // the graphic we'll flip when we change direction. We don't want collision to get affected by that
    [SerializeField] Transform graphicToFlip;
    // how long we spend tweening the x scale when we flip direction
    [SerializeField] float xFlipTweenTime = 0.3f;

    [SerializeField] List<SpriteResolver> equipmentSpriteResolvers;

    void Awake()
    {
        localPlayer = this;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Equip(EquipmentScrob equipment)
    {
        foreach (var equip in equipment.equipmentSelections)
        {
            foreach (var sr in equipmentSpriteResolvers)
            {
                if (sr.GetCategory() == equip.spriteCategory)
                {
                    sr.SetCategoryAndLabel(equip.spriteCategory, equip.spriteLabel);
                    return;
                }
            }
        }

        UpdateInfo();
    }

    private void Start()
    {
        UpdateInfo();
    }

    void Update()
    {
        HandleInput();
        ApplyDrag();
        ApplyVelocity();
    }

    void UpdateInfo()
    {
        infoBar.UpdateInfo();
    }

    public Sprite GetHeadSprite()
    {
        return headSprite.sprite;
    }

    void HandleInput()
    {
        if (ParchmentController.isOpen)
        {
            SetAnimationState(PlayerAnimState.Idle);
            return;
        }

        Vector2 movementInput = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
        HandleMovement(movementInput);
        ApplyDirection(movementInput);

        if (Input.GetKeyDown(KeyCode.E))
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
    }

    void ApplyDirection(Vector2 input)
    {
        // flipX doesn't work with the animations, we're going to use scaling instead

        if (input.x < -0.01 && graphicToFlip.localScale.x == 1)
        {
            graphicToFlip.DOScaleX(-1, xFlipTweenTime);
        }
        else if (input.x > 0.01 && graphicToFlip.localScale.x == -1)
        {
            graphicToFlip.DOScaleX(1, xFlipTweenTime);
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
        if (_interactionNotifier.closestInteractable != null)
        {
            _interactionNotifier.closestInteractable.OnInteraction?.Invoke();
            _animator.SetTrigger("Interact");
        }
    }

    void SetAnimationState(PlayerAnimState state)
    {
        if (state != _currentAnimationState)
        {
            _currentAnimationState = state;
            _animator.SetInteger("AnimationState", (int)_currentAnimationState);
        }
    }

    public void AddGold(int _amount)
    {
        inventory.gold += _amount;
        UpdateInfo();
    }

    public int GetGold()
    {
        return inventory.gold;
    }
}

// Explicitly setting the values here to keep this from getting messed up in serialization if it's changed in weird ways later
enum PlayerAnimState
{
    Idle = 0,
    Walking = 1,
}