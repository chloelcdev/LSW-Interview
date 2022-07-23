using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFadeController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text thanksText;

    private void Awake()
    {
        Color col = GetComponent<Image>().color;
        col.a = 1;
        GetComponent<Image>().color = col;
    }

    private void Start()
    {
        GetComponent<Image>().DOFade(0, 2);
    }

    public void DoEndGame()
    {
        Debug.Log("endgame");
        GetComponent<Image>().DOFade(1, 2).onComplete = () =>
        {
            thanksText.DOFade(1,2);
        };
    }
}
