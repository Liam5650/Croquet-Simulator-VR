using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : ButtonController
{
    private bool waitToLoad = false;
    public string sceneToLoad; //The Scene we would like to load

    public override void Execute()
    {
        BlackScreenController.instance.FadeTo(1f);
        waitToLoad = true;

        PlayerPrefs.DeleteAll();
        Debug.Log("Stats Reset");
    }

    private void Update()
    {
        if (waitToLoad && BlackScreenController.instance.GetAlpha() == 1f)
        {
            Debug.Log("Scene Reset");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}