using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : ButtonController
{
    // ResetButton - Resets the scene and all player stats

    private bool waitToLoad = false; // Reference for wether we have waited for other processes to finish before reseting
    public string sceneToLoad; //The Scene we would like to load

    // Start the fade, reset prefs
    public override void Execute()
    {
        BlackScreenController.instance.FadeTo(1f);
        waitToLoad = true;

        PlayerPrefs.DeleteAll();
        Debug.Log("Stats Reset");
    }

    // Wait for the screen to be black, then reload the scene
    private void Update()
    {
        if (waitToLoad && BlackScreenController.instance.GetAlpha() == 1f)
        {
            Debug.Log("Scene Reset");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}