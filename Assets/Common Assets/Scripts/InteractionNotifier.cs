using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InteractionNotifier : MonoBehaviour
{
    [SerializeField] TMP_Text _notificationText;
    [SerializeField] CanvasGroup _notificationTextCanvasGroup;
    string startingTextFormat;

    List<Interactable> nearbyInteractables = new List<Interactable>();

    [HideInInspector] public Interactable closestInteractable = null;

    [SerializeField] float messageFadeTime = 0.3f;

    private void Awake()
    {
        startingTextFormat = _notificationText.text;

        _notificationText.text = "";
        _notificationTextCanvasGroup.alpha = 0;
        _notificationTextCanvasGroup.gameObject.SetActive(false);
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

    public void UpdateClosestInteractable()
    {
        // find the closest interactable

        float closestDistance = 0;
        Interactable closestInteractableFound = null;

        foreach (var interactable in nearbyInteractables)
        {
            if (!interactable.IsInteractable) continue;

            float distance = Vector3.Distance(interactable.transform.position, transform.position);
            if (distance < closestDistance || closestInteractableFound == null)
            {
                closestInteractableFound = interactable;
                closestDistance = distance;
            }
            
        }

        // if the closest interactable changed, update which one is closest and set its message
        if (closestInteractableFound == null)
            SetMessage("");
        else if (closestInteractable != closestInteractableFound)
            SetMessage(closestInteractableFound.message);

        closestInteractable = closestInteractableFound;
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
            _notificationTextCanvasGroup.DOKill();
            _notificationTextCanvasGroup.gameObject.SetActive(true);
            _notificationTextCanvasGroup.DOFade(1, messageFadeTime).onComplete = () => { };
        }
        else if (!enabled)
        {
            _notificationTextCanvasGroup.DOKill();
            _notificationTextCanvasGroup.DOFade(0, messageFadeTime).onComplete = () => 
            {
                _notificationTextCanvasGroup.gameObject.SetActive(false);
            };
        }
    }
}
