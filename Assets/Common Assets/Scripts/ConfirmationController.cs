using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ConfirmationController : MonoBehaviour
{
    static ConfirmationController _instance;

    [SerializeField] CanvasGroup canvasGroup;
    float fadeTime = 0.3f;

    [SerializeField] TMP_Text Title;
    [SerializeField] TMP_Text Message;

    [SerializeField] DeepButonHandler LeftButton;
    [SerializeField] DeepButonHandler RightButton;

    [SerializeField] DeepButonHandler SingleButton;

    private void Awake()
    {
        _instance = this;

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public static void Open(string title, string message, string leftButtonText, string rightButtonText, Action onLeftButton, Action onRightButton)
    {
        _instance.DoOpen(title, message, leftButtonText, rightButtonText, onLeftButton, onRightButton);
    }

    public void DoOpen(string title, string message, string leftButtonText, string rightButtonText, Action onLeftButton, Action onRightButton)
    {
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);

        SingleButton.gameObject.SetActive(false);

        Title.text = title;
        Message.text = message;

        LeftButton.SetText(leftButtonText);
        RightButton.SetText(rightButtonText);

        LeftButton.button.onClick.RemoveAllListeners();
        RightButton.button.onClick.RemoveAllListeners();

        LeftButton.button.onClick.AddListener(() =>
        {
            if (onLeftButton != null)
                onLeftButton();

            Close();
        });

        RightButton.button.onClick.AddListener(() =>
        {
            if (onRightButton != null)
                onRightButton();

            Close();
        });

        FadeIn();
    }

    public static void Open(string title, string message, string buttonText, Action onButton)
    {
        _instance.DoOpenSingle(title, message, buttonText, onButton);
    }

    public void DoOpenSingle(string title, string message, string buttonText, Action onButton)
    {
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);

        SingleButton.gameObject.SetActive(true);

        Title.text = title;
        Message.text = message;

        SingleButton.SetText(buttonText);

        SingleButton.button.onClick.RemoveAllListeners();
        SingleButton.button.onClick.AddListener(() =>
        {
            if (onButton != null)
                onButton();

            Close();
        });

        FadeIn();
    }

    void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime);
    }

    void Close()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, fadeTime);
    }

}
