using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerStatsAndEquipsScrob : ScriptableObject
{
    public int gold = 0;

    public EquipmentScrob equippedHead;
    public EquipmentScrob equippedTorso;
    public EquipmentScrob equippedGloves;

    // put a list of clothing items we own here later
}
