using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDirectory : ScriptableObject
{
    List<EquipmentScrob> AllEquipment = new List<EquipmentScrob>();

    void GetShopEquipment()
    {
        List<EquipmentScrob> allShopEquipment = new List<EquipmentScrob>();

        foreach (var item in AllEquipment)
        {
            if (!item.IsDefault)
            {
                allShopEquipment.Add(item);
            }
        }
    }
}
