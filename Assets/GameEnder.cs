using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField] ScreenFadeController fadeController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.GetComponent<PlayerController>() != null)
        {
            fadeController.DoEndGame();
        }
    }
}
