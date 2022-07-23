using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu()]
public class EquipmentScrob : ScriptableObject
{
    public SpriteLibraryAsset library;
    public bool IsDefault = false;

    public List<EquipmentSelection> equipmentSelections = new List<EquipmentSelection>();
    
}

[System.Serializable]
public struct EquipmentSelection
{
    public EquipmentSelection(string category, string label, int categoryIndex, int labelIndex)
    {
        spriteCategory = category;
        spriteLabel = label;

        spriteCategoryIndex = categoryIndex;
        spriteLabelIndex = labelIndex;
    }

    [HideInInspector] public string spriteCategory;
    [HideInInspector] public string spriteLabel;

    [HideInInspector] public int spriteCategoryIndex;
    [HideInInspector] public int spriteLabelIndex;
}
