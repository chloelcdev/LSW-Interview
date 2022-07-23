using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu()]
public class EquipmentScrob : ScriptableObject
{
    public SpriteLibraryAsset library;

    public EquipmentType type = EquipmentType.Torso;

    public int cost = 2;
    public string itemName = "Equipment Piece";

    public bool IsDefault = false;
    public bool IsOwned = false;

    public List<EquipmentSelection> equipmentSelections = new List<EquipmentSelection>();

    public Sprite GetFirstSprite()
    {
        return library.GetSprite(equipmentSelections[0].spriteCategory, equipmentSelections[0].spriteLabel);
    }

    /*public List<EquipmentInfo> GetInfo() 
    {
        List<EquipmentInfo> infoList = new List<EquipmentInfo>();

        foreach (var equipmentSelection in equipmentSelections)
        {
            EquipmentInfo info = new EquipmentInfo();
            info.sprite = library.GetSprite(equipmentSelection.spriteCategory, equipmentSelection.spriteLabel);
            info.cost = cost;
            info.itemName = itemName;

            infoList.Add(info);
        }

        return infoList;
    }*/
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

public enum EquipmentType
{
    Head,
    Torso,
    Gloves
}

// equipment info is what we actually work with, it gives us sprites and stuff we can actually use, along with the equipment selection in case we need it
/*public class EquipmentInfo
{
    public string itemName;
    public Sprite sprite;
    public int cost;

    [HideInInspector] public EquipmentSelection equipmentSelection;
}*/