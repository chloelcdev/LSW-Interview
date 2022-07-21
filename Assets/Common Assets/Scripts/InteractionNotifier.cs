using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InteractionNotifier : MonoBehaviour
{
    [SerializeField] TMP_Text _notificationText;
    string startingTextFormat;

    List<Interactable> nearbyInteractables = new List<Interactable>();

    [HideInInspector] public Interactable closestInteractable = null;

    [SerializeField] float messageFadeTime = 0.3f;

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
            SetMessage("");
            return;
        }

        // find the closest interactable

        float closestDistance = 0;
        Interactable closestInteractableFound = null;

        foreach (var interactable in nearbyInteractables)
        {
            float distance = Vector3.Distance(interactable.transform.position, transform.position);
            if (distance < closestDistance || closestInteractableFound == null)
            {
                closestInteractableFound = interactable;
                closestDistance = distance;
            }
            
        }

        // if the closest interactable changed, update which one is closest and set its message

        if (closestInteractable != closestInteractableFound)
        {
            closestInteractable = closestInteractableFound;

            SetMessage(closestInteractable.message);
        }
    }

    void SetMessage(string text)
    {
        bool enabled = false;

        if (text != "")
        {
            enabled = true;
            _notificationText.text = string.Format(startingTextFormat, text);
        }

        // do a nice little fade when we enable/disable it

        if (enabled)
        {
            _notificationText.DOKill();
            _notificationText.gameObject.SetActive(true);
            _notificationText.DOFade(1, messageFadeTime).onComplete = () => { };
        }
        else if (!enabled)
        {
            _notificationText.DOKill();
            _notificationText.DOFade(0, messageFadeTime).onComplete = () => 
            {
                _notificationText.gameObject.SetActive(false);
            };
        }
    }
}
