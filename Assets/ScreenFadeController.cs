using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFadeController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text thanksText;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        Color col = image.color;
        col.a = 1;
        image.color = col;
    }

    private void Start()
    {
        image.DOFade(0, 2);
    }

    public void DoEndGame()
    {
        Debug.Log("endgame");
        image.raycastTarget = true;
        image.DOFade(1, 2).onComplete = () =>
        {
            thanksText.DOFade(1,2);
        };
    }
}
