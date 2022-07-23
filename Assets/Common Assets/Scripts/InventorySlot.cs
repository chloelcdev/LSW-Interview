using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public RectTransform rectTransform;

    public InventoryItem currentItem = null;

    public bool isShopInventorySlot = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void CreateItem(InventoryItem itemPrefab, EquipmentScrob item)
    {
        var spawnedItem = Instantiate(itemPrefab, transform);
        spawnedItem.Setup(item, this);

        SetItem(spawnedItem);
    }

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
    }
}
