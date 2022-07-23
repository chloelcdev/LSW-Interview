using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(EquipmentScrob))]
 public class EquipmentScrobEditor : Editor
{
    int[] _spriteCategoryIndexes = new int[0];
    int[] _spriteLabelIndexes = new int[0];

    public override void OnInspectorGUI()
    {
        var scrob = (EquipmentScrob)target;
        int selectionsCount = scrob.equipmentSelections.Count();

        if (_spriteCategoryIndexes.Length != selectionsCount)
        {
            _spriteCategoryIndexes = new int[selectionsCount];
            for (int i = 0; i < selectionsCount; i++)
            {
                _spriteCategoryIndexes[i] = scrob.equipmentSelections[i].spriteCategoryIndex;
            }
        }

        if (_spriteLabelIndexes.Length != selectionsCount)
        {
            _spriteLabelIndexes = new int[selectionsCount];
            for (int i = 0; i < selectionsCount; i++)
            {
                _spriteLabelIndexes[i] = scrob.equipmentSelections[i].spriteLabelIndex;
            }
        }

        string[] categories = scrob.library.GetCategoryNames().ToArray();


        // Draw the default inspector
        DrawDefaultInspector();

        for (int i = 0; i < selectionsCount; i++)
        {
            _spriteCategoryIndexes[i] = EditorGUILayout.Popup(_spriteCategoryIndexes[i], categories);


            string[] spriteLabels = scrob.library.GetCategoryLabelNames(categories[_spriteCategoryIndexes[i]]).ToArray();

            _spriteLabelIndexes[i] = EditorGUILayout.Popup(_spriteLabelIndexes[i], spriteLabels);

            //scrob.equipmentSelections[i] = new EquipmentSelection(categories[_spriteCategoryIndexes[i]], spriteLabels[_spriteLabelIndexes[i]]);

            List<Texture2D> textures = new List<Texture2D>();
            foreach (var label in spriteLabels)
            {
                textures.Add(AssetPreview.GetAssetPreview(scrob.library.GetSprite(categories[_spriteCategoryIndexes[i]], label)));
            }

            _spriteLabelIndexes[i] = GUILayout.SelectionGrid(_spriteLabelIndexes[i], textures.ToArray(), 4, GUILayout.Height(128));

            string selectedCategory = categories[_spriteCategoryIndexes[i]];
            string selectedLabel = spriteLabels[_spriteLabelIndexes[i]];

            if (scrob.equipmentSelections.Count > i)
                scrob.equipmentSelections[i] = new EquipmentSelection(selectedCategory, selectedLabel, _spriteCategoryIndexes[i], _spriteLabelIndexes[i]);
        }

        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}