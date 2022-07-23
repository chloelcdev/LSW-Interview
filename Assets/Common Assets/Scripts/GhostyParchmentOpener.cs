using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GhostyParchmentOpener : MonoBehaviour
{
    [SerializeField] string title;

    [SerializeField] string preConditionMessage;
    [SerializeField] string postConditionMessage;

    [SerializeField] Collider2D ghostyCollider;
    [SerializeField] SpriteRenderer ghostyRenderer;

    [SerializeField] GameObject giantStone;

    bool conditionMet => PlayerController.localPlayer.statsAndEquips.equipment[EquipmentType.Head].itemName != "Red Hairdo";

    public void OpenParchment()
    {
        string message = conditionMet ? postConditionMessage : preConditionMessage;
        ParchmentController.OpenParchment(title, message, () => {
            if (conditionMet)
                DoGhostlyActions();
        });
    }

    void DoGhostlyActions()
    {
        ghostyCollider.enabled = false;
        SFXPlayer.PlayGhostSound();
        ghostyRenderer.DOFade(0, 1.3f);

        Invoke("OpenStonePath", 2f);
    }

    void OpenStonePath()
    {
        SFXPlayer.PlayStoneSlideSound();
        giantStone.SetActive(false);
    }
}
