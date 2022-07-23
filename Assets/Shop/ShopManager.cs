using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] EquipmentDirectory equipmentDirectory;

    [SerializeField] InventoryItem inventoryItemPrefab;

    [SerializeField] List<InventorySlot> playerInventorySlots = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> shopInventorySlots = new List<InventorySlot>();

    public void Populate()
    {
        foreach(var item in equipmentDirectory.GetPlayerEquipment())
        {
            AddItem(item, playerInventorySlots);
        }

        foreach (var item in equipmentDirectory.GetPlayerEquipment())
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
            }
        }
    }

    void Purchase(EquipmentScrob item)
    {

    }

    void Sell(EquipmentScrob item)
    {

    }

}
