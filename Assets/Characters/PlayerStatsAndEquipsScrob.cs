using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerStatsAndEquipsScrob : ScriptableObject
{
    public int gold = 0;

    public List<EquipmentScrob> equipment;

    // put a list of clothing items we own here later
}
