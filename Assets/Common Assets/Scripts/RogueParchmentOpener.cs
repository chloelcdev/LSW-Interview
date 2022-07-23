using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class RogueParchmentOpener : MonoBehaviour
{
    [SerializeField] string title;

    [SerializeField] string preConditionMessage;
    [SerializeField] string postConditionMessage;

    [SerializeField] GameObject giantStone;

    bool conditionMet => !giantStone.activeInHierarchy;

    public void OpenParchment()
    {
        string message = conditionMet ? postConditionMessage : preConditionMessage;
        ParchmentController.OpenParchment(title, message, () => { });
    }

}
