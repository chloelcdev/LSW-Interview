using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EquipmentDirectory : ScriptableObject
{
    public List<EquipmentScrob> AllEquipment = new List<EquipmentScrob>();

    public List<EquipmentScrob> GetShopEquipment()
    {
        List<EquipmentScrob> equipment = new List<EquipmentScrob>();

        foreach (var item in AllEquipment)
        {
            if (!item.IsDefault && !item.IsOwned)
            {
                equipment.Add(item);
            }
        }

        return equipment;
    }

    public List<EquipmentScrob> GetPlayerEquipment()
    {
        List<EquipmentScrob> equipment = new List<EquipmentScrob>();

        foreach (var item in AllEquipment)
        {
            if (item.IsDefault || item.IsOwned)
            {
                equipment.Add(item);
            }
        }

        return equipment;
    }

    public List<EquipmentScrob> GetPlayerEquipment(EquipmentType ofType)
    {
        List<EquipmentScrob> equipment = new List<EquipmentScrob>();

        foreach (var item in AllEquipment)
        {
            if (item.IsDefault || item.IsOwned)
            {
                if (item.type == ofType)
                    equipment.Add(item);
            }
        }

        return equipment;
    }
}
