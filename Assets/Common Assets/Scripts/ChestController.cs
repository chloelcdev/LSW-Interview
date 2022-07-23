using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] int _rewardAmount = 1;
    Animator _animator;
    Interactable _interactable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _interactable = GetComponent<Interactable>();
    }
    
    public void OnInteract()
    {
        GiveReward(_rewardAmount);
        _animator.SetTrigger("Open");
        _interactable.IsInteractable = false;
    }

    void GiveReward(int _amount)
    {
        PlayerController.localPlayer.AddGold(_amount);
        GetComponent<AudioSource>().Play();
    }
}
