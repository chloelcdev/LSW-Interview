using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            DoExit();
    }

    public void DoExit()
    {
        ConfirmationController.Open
        (
            "Exit Game?",
            "Are you done playing?",
            "Exit", "Cancel",
            () => Application.Quit(),
            null
        );
    }
}
