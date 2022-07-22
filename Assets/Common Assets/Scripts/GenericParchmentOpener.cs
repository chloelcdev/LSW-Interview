using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericParchmentOpener : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string message;

    [SerializeField] UnityEvent onMessageClose = new UnityEvent();

    public void OpenParchment()
    {
        ParchmentController.OpenParchment(title, message, () => onMessageClose?.Invoke());
    }
}
