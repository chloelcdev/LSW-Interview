using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBarController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text goldAmount;
    [SerializeField] Image headshot;

    public void UpdateInfo()
    {
        goldAmount.text = PlayerController.localPlayer.GetGold().ToString();
        headshot.sprite = PlayerController.localPlayer.GetHeadSprite();
        headshot.SetNativeSize();
    }
}
