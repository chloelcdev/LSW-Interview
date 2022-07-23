using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class DeepButonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    [SerializeField] Image nonPressedGraphic;
    [SerializeField] TMP_Text nonPressedLabel;
    [SerializeField] TMP_Text pressedLabel;

    [SerializeField] string Label = "Button";
    [SerializeField] Color LabelColor = Color.white;
    [SerializeField] float LabelFontSize = 28;

    bool pointerIsDown = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        UpdateDeepButtonLabels();
        UpdateGraphicState();
    }

    void UpdateGraphicState()
    {
        if (!button.interactable)
            nonPressedGraphic.gameObject.SetActive(false);
        else
            nonPressedGraphic.gameObject.SetActive(!pointerIsDown);
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        UpdateGraphicState();
    }

    private void OnValidate()
    {
        UpdateDeepButtonLabels();
    }

    void UpdateDeepButtonLabels()
    {
        nonPressedLabel.text = Label;
        pressedLabel.text = Label;

        nonPressedLabel.fontSize = LabelFontSize;
        pressedLabel.fontSize = LabelFontSize;

        nonPressedLabel.color = LabelColor;
        pressedLabel.color = LabelColor;
    }

    public void SetText(string text)
    {
        Label = text;
        UpdateDeepButtonLabels();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerIsDown = true;
        UpdateGraphicState();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerIsDown = false;
        UpdateGraphicState();
    }
}
