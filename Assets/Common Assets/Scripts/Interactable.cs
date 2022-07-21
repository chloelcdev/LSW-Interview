using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteraction = new UnityEvent();

    /// <summary>
    /// Message will show up as "[SpaceBar] [message]"
    /// </summary>
    public string message = "Interact";
}
