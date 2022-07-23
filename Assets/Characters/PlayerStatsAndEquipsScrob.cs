using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerStatsAndEquipsScrob : ScriptableObject
{
    public int gold = 0;

    public Dictionary<EquipmentType, EquipmentScrob> equipment = new Dictionary<EquipmentType, EquipmentScrob>();

    public List<EquipmentScrob> defaultEquipment = new List<EquipmentScrob>();

    public void OnCharacterSpawn()
    {
        equipment.Clear();

        foreach (var item in defaultEquipment)
        {
            equipment.Add(item.type, item);
        }
    }
}
