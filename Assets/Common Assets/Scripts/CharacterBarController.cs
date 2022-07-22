using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBarController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text goldAmount;

    private void Update()
    {
        goldAmount.text = PlayerController.localPlayer.GetGold().ToString();
    }
}
