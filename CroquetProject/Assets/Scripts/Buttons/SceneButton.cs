using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : ButtonController
{
    public string sceneToLoad; //The Scene we would like to load

    //Perform button press execution funtion, ie load new scene
    public override void Execute()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
