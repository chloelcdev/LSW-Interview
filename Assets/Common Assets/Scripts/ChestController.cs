using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] int _rewardAmount = 1;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void OnInteract()
    {
        GiveReward(_rewardAmount);
        _animator.SetTrigger("Open");
    }

    void GiveReward(int _amount)
    {

    }
}
