using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterScreenController : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float fadeTime = 0.3f;

    [SerializeField] Button characterScreenOpenButton;

    [SerializeField] List<EquipmentSelector> selectors = new List<EquipmentSelector>();

    bool isOpen = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            ToggleOpen();
        }
    }

    void ToggleOpen()
    {
        if (!UILocator.UIIsOpen())
            FadeIn();
        else if (isOpen)
            Close();
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        characterScreenOpenButton.onClick.AddListener(ToggleOpen);
    }

    public void FadeIn()
    {
        foreach (var selector in selectors)
        {
            selector.FormatToPlayer();
        }

        isOpen = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime);
    }

    public void Close()
    {
        isOpen = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, fadeTime);
        SFXPlayer.PlayExitSound();
    }
}
