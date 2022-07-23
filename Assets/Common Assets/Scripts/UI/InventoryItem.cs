using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool dragging = false;
    float distance;

    [HideInInspector] public ShopManager shop;

    [SerializeField] EquipmentScrob equipmentData;

    [SerializeField] Image icon;
    [SerializeField] Image overlay;

    [SerializeField] Image equippedIcon;

    [SerializeField] TMPro.TMP_Text goldCostLabel;

    RectTransform rectTransform;

    InventorySlot currentSlot = null;

    // this will keep the ui element visible over everything else while it's mid-drag
    RectTransform dragZone;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        overlay.enabled = false;

        dragZone = UILocator.LocateUI("dragzone");
    }

    public void Setup(EquipmentScrob data, InventorySlot slot)
    {
        currentSlot = slot;

        equipmentData = data;
        SetEquippedStatus();

        goldCostLabel.text = data.cost.ToString();

        SetIcon(data.GetFirstSprite());
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        overlay.sprite = sprite;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (!equipmentData.IsEquipped)
            overlay.enabled = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!dragging)
            overlay.enabled = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!equipmentData.IsEquipped)
            StartDrag();
        else
        {
            ConfirmationController.Open
            (
                "No can do!",
                $"You can't sell something you're currently wearing!",
                "Okay", null
            );

            SFXPlayer.PlayFailureSound();
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (dragging)
        {
            overlay.enabled = false;

            Drop();
        }
    }

    void StartDrag()
    {
        dragging = true;
        rectTransform.SetParent(dragZone);
        equippedIcon.gameObject.SetActive(false);
    }


    void Drop()
    {
        dragging = false;

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            var slot = hit.collider.GetComponent<InventorySlot>();
            if (slot != null && slot.currentItem == null)
            {
                if (currentSlot.isShopInventorySlot == slot.isShopInventorySlot)
                {
                    SetSlot(slot);
                }
                else if (slot.isShopInventorySlot) {
                    shop.TrySell(this, equipmentData, slot);
                }
                else
                {
                    shop.TryBuy(this, equipmentData, slot);
                }

                return;
            }
        }

        ResetDraggable();
    }

    public void SetEquippedStatus()
    {
        equippedIcon.gameObject.SetActive(equipmentData.IsEquipped);
    }

    public void ResetDraggable()
    {
        rectTransform.SetParent(currentSlot.rectTransform);
        rectTransform.anchoredPosition = Vector2.zero;

        SetEquippedStatus();
    }

    public void SetSlot(InventorySlot slot)
    {
        rectTransform.SetParent(slot.rectTransform);
        rectTransform.anchoredPosition = Vector2.zero;

        SetEquippedStatus();

        currentSlot.currentItem = null;
        slot.currentItem = this;

        currentSlot = slot;
    }

    void Update()
    {
        if (dragging)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}