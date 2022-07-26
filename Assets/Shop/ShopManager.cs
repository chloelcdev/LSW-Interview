using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void On_X_Button()
    {
        Close();
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
        SFXPlayer.PlayPurchaseSound();
    }

    void Sell(EquipmentScrob item)
    {
        PlayerController.localPlayer.AddGold(item.cost);
        item.IsOwned = false;
        SFXPlayer.PlayPurchaseSound();
    }

    public void TryBuy(InventoryItem item, EquipmentScrob equipmentData, InventorySlot slotTo)
    {
        if (PlayerController.localPlayer.GetGold() >= equipmentData.cost)
        {
            ConfirmationController.Open
            (
                "Are you sure?",
                $"You're about to buy {equipmentData.itemName} for {equipmentData.cost} gold!",
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
                "No can do!",
                $"You need {equipmentData.cost} gold but you have {PlayerController.localPlayer.GetGold()} gold!",
                "Okay :(", null
            );

            SFXPlayer.PlayFailureSound();
        }
    }

    public void TrySell(InventoryItem item, EquipmentScrob equipmentData, InventorySlot slotTo)
    {
        if (!equipmentData.IsEquipped)
        {
            ConfirmationController.Open
            (
                "Are you sure?",
                $"You're about to sell {equipmentData.itemName} for {equipmentData.cost} gold!",
                "Cancel", "Sell",
                item.ResetDraggable,
                () =>
                {
                    item.SetSlot(slotTo);
                    Sell(equipmentData);
                }
            );
        }
        else
        {
            item.ResetDraggable();

            ConfirmationController.Open
            (
                "No can do!",
                $"You can't sell something you're currently wearing!",
                "Okay :(", null
            );

            SFXPlayer.PlayFailureSound();
        }
    }

    public void UpdateEquippedStatuses()
    {
        // run through and update the equipped icon for the inventorys
        foreach (var slot in playerInventorySlots)
        {
            if (slot.currentItem != null)
            {
                slot.currentItem.SetEquippedStatus();
            }
        }

        // run through and update the equipped icon for the inventorys
        foreach (var slot in shopInventorySlots)
        {
            if (slot.currentItem != null)
            {
                slot.currentItem.SetEquippedStatus();
            }
        }
    }

    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime);

        UpdateEquippedStatuses();

        // Don't pop here, we're already playing it from the parchment closing
        //SFXPlayer.PlayPopSound();
    }

    void Close()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, fadeTime);
        SFXPlayer.PlayPopSound();
    }

}
