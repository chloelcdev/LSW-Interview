using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu()]
public class EquipmentScrob : ScriptableObject
{
    public SpriteLibraryAsset library;

    public int cost = 2;

    public bool IsDefault = false;
    public bool IsOwned = false;

    public List<EquipmentSelection> equipmentSelections = new List<EquipmentSelection>();

    public List<EquipmentInfo> GetInfo() 
    {
        List<EquipmentInfo> infoList = new List<EquipmentInfo>();

        foreach (var equipmentSelection in equipmentSelections)
        {
            EquipmentInfo info = new EquipmentInfo();
            info.sprite = library.GetSprite(equipmentSelection.spriteCategory, equipmentSelection.spriteLabel);
            info.cost = cost;

            infoList.Add(info);
        }

        return infoList;
    }
}

// equipment selection just holds the name and index of the info needed for Unity's SpriteResolver
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

// equipment info is what we actually work with, it gives us sprites and stuff we can actually use, along with the equipment selection in case we need it
public class EquipmentInfo
{
    public Sprite sprite;
    public int cost;

    [HideInInspector] public EquipmentSelection equipmentSelection;
}