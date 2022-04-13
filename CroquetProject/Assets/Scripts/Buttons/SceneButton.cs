using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : ButtonController
{
    // SceneButton - loads a scene defined by ‘sceneToLoad’ when triggered

    public string sceneToLoad; //The Scene we would like to load
    private bool waitToLoad = false; // Reference for wether we have waited for other processes to finish before loading

    //Perform button press execution funtion, ie load new scene
    public override void Execute()
    {
        BlackScreenController.instance.FadeTo(1f);
        waitToLoad = true;
    }

    // Wait until the screen is black before loading
    private void Update()
    {
        if (waitToLoad && BlackScreenController.instance.GetAlpha() == 1f)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
