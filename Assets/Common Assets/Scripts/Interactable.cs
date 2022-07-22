using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    bool _isInteractable = true;
    public bool IsInteractable 
    {
        get
        {
            return _isInteractable;
        }

        set 
        {
            _isInteractable = value;
            PlayerController.localPlayer._interactionNotifier.UpdateClosestInteractable();
        }
    }

    public UnityEvent OnInteraction = new UnityEvent();

    /// <summary>
    /// Message will show up as "[SpaceBar] [message]"
    /// </summary>
    public string message = "Interact";
}
