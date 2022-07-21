using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionNotifier : MonoBehaviour
{
    [SerializeField] TMP_Text _notificationText;
    string startingTextFormat;

    List<Interactable> nearbyInteractables = new List<Interactable>();

    [HideInInspector] public Interactable closestInteractable = null;

    private void Awake()
    {
        startingTextFormat = _notificationText.text;
        _notificationText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            // try to get the interactable from either the gameobject or its parent
            var interactable = collision.gameObject.GetComponent<Interactable>();
            if (interactable == null && collision.transform.parent != null) interactable = collision.transform.parent.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (!nearbyInteractables.Contains(interactable))
                    nearbyInteractables.Add(interactable);

                UpdateClosestInteractable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            // try to get the interactable from either the gameobject or its parent
            var interactable = collision.gameObject.GetComponent<Interactable>();
            if (interactable == null && collision.transform.parent != null) interactable = collision.transform.parent.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (nearbyInteractables.Contains(interactable))
                    nearbyInteractables.Remove(interactable);

                UpdateClosestInteractable();
            }
        }
    }

    void UpdateClosestInteractable()
    {
        // if there are no nearby interactables, take the message away

        if (nearbyInteractables.Count == 0)
        {
            closestInteractable = null;
            SetMessage("", false);
            return;
        }

        // find the closest interactable

        float closestDistance = 0;

        foreach (var interactable in nearbyInteractables)
        {
            float distance = Vector3.Distance(interactable.transform.position, transform.position);
            if (distance < closestDistance || closestInteractable == null)
            {
                closestInteractable = interactable;
                closestDistance = distance;
            }
            
        }

        // set the message of the closest interactable

        SetMessage(closestInteractable.message, true);
    }

    void SetMessage(string text, bool enabled)
    {
        _notificationText.text = string.Format(startingTextFormat, text);
        _notificationText.gameObject.SetActive(enabled);
    }
}
