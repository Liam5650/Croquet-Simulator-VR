using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : ButtonController
{
    private GameObject currentMenu;
    public string sceneToLoad; //The Scene we would like to load

    public override void Execute()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Stats Reset");

        currentMenu = transform.parent.gameObject;
        currentMenu.SetActive(false);
        SceneManager.LoadScene(sceneToLoad);
    }
}
