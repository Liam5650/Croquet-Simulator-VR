using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ButtonController
{
    private GameObject currentMenu;
    public GameObject menuToLoad;

    //Perform button press execution funtion, for this it switches the menu
    public override void Execute()
    {
        currentMenu = transform.parent.gameObject;
        currentMenu.SetActive(false);
        menuToLoad.SetActive(true);
    }
}
