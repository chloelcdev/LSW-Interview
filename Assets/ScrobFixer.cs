using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScrobFixer : MonoBehaviour
{
    public PlayerStatsAndEquipsScrob statsAndEquipsScrob;
    public EquipmentDirectory equipmentDirectory;

    // if we're in editor we have to clear the dumb saved values that scrobs hold onto (doesn't happen in build so we just need this for testing purposes)
    // in a full project I would avoid using scrobs for runtime data because they can be accidentally saved and uploaded.

#if UNITY_EDITOR
    void Start()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.ExitingPlayMode)
        {
            statsAndEquipsScrob.gold = 0;

            foreach (var item in equipmentDirectory.AllEquipment)
            {
                item.IsOwned = item.IsDefault;
                item.IsEquipped = item.IsDefault;
            }

            EditorApplication.playModeStateChanged -= ModeChanged;
        }
    }
#endif
}