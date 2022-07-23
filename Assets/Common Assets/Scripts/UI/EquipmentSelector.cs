using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelector : MonoBehaviour
{
    [SerializeField] EquipmentDirectory equipmentDirectory;

    [SerializeField] DeepButonHandler equipButton;

    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;

    [SerializeField] Image equipmentImage;

    [SerializeField] EquipmentType equipmentType;

    EquipmentScrob currentSelection;
    int selectionIndex = 0;

    private void Awake()
    {
        nextButton.onClick.AddListener(On_Next_Clicked);
        prevButton.onClick.AddListener(On_Previous_Clicked);
        equipButton.button.onClick.AddListener(On_Equip_Clicked);
    }

    public void FormatToPlayer()
    {
        EquipmentScrob equip = PlayerController.localPlayer.statsAndEquips.equipment[equipmentType];

        selectionIndex = GetIndexOfItem(equip);
        Format(equip);
    }

    int GetIndexOfItem(EquipmentScrob equip)
    {
        var playerEquipment = equipmentDirectory.GetPlayerEquipment(equipmentType);

        int index = 0;
        foreach (var item in playerEquipment)
        {
            if (item == equip)
                break;

            index++;
        }

        return index;
    }

    public void Format(EquipmentScrob equip)
    {
        equipmentImage.sprite = equip.GetFirstSprite();

        Debug.Log(equip.IsEquipped);
        equipButton.SetInteractable(!equip.IsEquipped);

        currentSelection = equip;
    }

    public void On_Next_Clicked()
    {
        selectionIndex++;
        var playerEquipment = equipmentDirectory.GetPlayerEquipment(equipmentType);

        if (selectionIndex > playerEquipment.Count - 1)
            selectionIndex = 0;

        Format(playerEquipment[selectionIndex]);
    }
    public void On_Previous_Clicked()
    {
        selectionIndex--;
        var playerEquipment = equipmentDirectory.GetPlayerEquipment(equipmentType);

        if (selectionIndex < 0)
            selectionIndex = playerEquipment.Count - 1;

        Format(playerEquipment[selectionIndex]);
    }

    public void On_Equip_Clicked()
    {
        PlayerController.localPlayer.Equip(currentSelection);
        Format(currentSelection);
    }
}
