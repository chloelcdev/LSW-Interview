using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Events;

public class MenuOptions : Editor
{
    [MenuItem("Tools/Custom/Make Readable Interactable", false, -1)]
    public static void MakeReadableInteractable()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogError("No GameObject selected!");
            return;
        }

        var interactable = Selection.activeGameObject.GetComponent<Interactable>();
        if (interactable == null) interactable = Selection.activeGameObject.AddComponent<Interactable>();

        var parchmentOpener = Selection.activeGameObject.GetComponent<GenericParchmentOpener>();
        if (parchmentOpener == null) parchmentOpener = Selection.activeGameObject.AddComponent<GenericParchmentOpener>();

        // check and see if OpenParchment is already being called from the Interactable, if it is print an error and don't go further
        for (int i = 0; i < interactable.OnInteraction.GetPersistentEventCount(); i++)
        {
            bool targetIsOpener = (interactable.OnInteraction.GetPersistentTarget(i) != parchmentOpener);
            Debug.Log($"Method name was {interactable.OnInteraction.GetPersistentMethodName(i)}");
            bool methodIsOpenFunc = (interactable.OnInteraction.GetPersistentMethodName(i) != "OpenParchment");

            if (targetIsOpener && methodIsOpenFunc)
            {
                Debug.LogError("Selected gameobject is already set up as an Interactable with Generic Parchment");
                return;
            }
        }

        interactable.message = "Read";
        UnityEventTools.AddPersistentListener(interactable.OnInteraction, parchmentOpener.OpenParchment);
    }
}
