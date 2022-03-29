using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : ButtonController
{
    public string sceneToLoad; //The Scene we would like to load
    private bool waitToLoad = false;

    //Perform button press execution funtion, ie load new scene
    public override void Execute()
    {
        BlackScreenController.instance.FadeTo(1f);
        waitToLoad = true;
    }

    private void Update()
    {
        if (waitToLoad && BlackScreenController.instance.GetAlpha() == 1f)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
