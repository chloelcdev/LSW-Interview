using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicBounce : MonoBehaviour
{
    [SerializeField] float _bounceAmount = 5f;
    [SerializeField] float _bounceTime = 0.7f;

    private void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + _bounceAmount, _bounceTime).SetLoops(-1, LoopType.Yoyo);
    }
}
