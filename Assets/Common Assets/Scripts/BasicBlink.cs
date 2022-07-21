using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicBlink : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    SpriteRenderer _spriteRenderer;

    [SerializeField] float FadeTime = 1;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_spriteRenderer != null)
            _spriteRenderer.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
        else if (_canvasGroup != null)
            _spriteRenderer.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
        else
            Debug.LogWarning($"BasicBlink exists on object {gameObject.name} but no CanvasGroup or SpriteRenderer is set");
    }
}
