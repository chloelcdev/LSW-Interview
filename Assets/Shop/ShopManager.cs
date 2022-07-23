using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    float fadeTime = 0.3f;

    [SerializeField] EquipmentDirectory equipmentDirectory;

    [SerializeField] InventoryItem inventoryItemPrefab;

    [SerializeField] List<InventorySlot> playerInventorySlots = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> shopInventorySlots = new List<InventorySlot>();

    public void Awake()
    {
        Populate();
    }

    public void Populate()
    {
        foreach(var item in equipmentDirectory.GetPlayerEquipment())
        {
            AddItem(item, playerInventorySlots);
        }

        foreach (var item in equipmentDirectory.GetShopEquipment())
        {
            AddItem(item, shopInventorySlots);
        }
    }

    void AddItem(EquipmentScrob item, List<InventorySlot> inventory)
    {
        foreach (var slot in inventory)
        {
            if (slot.currentItem == null)
            {
                slot.shop = this;
                slot.CreateItem(inventoryItemPrefab, item);
                return;
            }
        }
    }

    void Buy(EquipmentScrob item)
    {
        PlayerController.localPlayer.AddGold(-item.cost);
        item.IsOwned = true;
    }

    void Sell(EquipmentScrob item)
    {
        PlayerController.localPlayer.AddGold(item.cost);
        item.IsOwned = false;

    }

    public void TryBuy(InventoryItem item, EquipmentScrob equipmentData, InventorySlot slotTo)
    {
        if (PlayerController.localPlayer.GetGold() >= equipmentData.cost)
        {
            ConfirmationController.Open
            (
                "Are you sure?",
                $"You're about to buy {equipmentData.itemName} for {equipmentData.cost}!",
                "Cancel", "Buy",
                item.ResetDraggable,
                () =>
                {
                    item.SetSlot(slotTo);
                    Buy(equipmentData);
                }
            );
        }
        else
        {
            item.ResetDraggable();

            ConfirmationController.Open
            (
                "Nope",
                $"You need {equipmentData.cost} gold but you only have {PlayerController.localPlayer.GetGold()} gold!",
                "Okay :(", null
            );
        }
    }

    public void TrySell(InventoryItem item, EquipmentScrob equipmentData, InventorySlot slotTo)
    {
        ConfirmationController.Open
        (
            "Are you sure?",
            $"You're about to sell {equipmentData.itemName} for {equipmentData.cost}!",
            "Cancel", "Sell",
            item.ResetDraggable,
            () =>
            {
                item.SetSlot(slotTo);
                Sell(equipmentData);
            }
        );
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
