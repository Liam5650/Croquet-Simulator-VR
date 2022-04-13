using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : ButtonController
{
    // QuitButton - sends an application.quit signal
    public override void Execute()
    {
        Application.Quit();

        Debug.Log("Game Quit");
    }
}
