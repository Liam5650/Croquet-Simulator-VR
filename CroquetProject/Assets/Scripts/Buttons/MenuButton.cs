using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ButtonController
{
    public GameObject currentMenu;
    public GameObject menuToLoad;

    //Perform button press execution funtion, for this it switches the menu
    public override void Execute()
    {
        currentMenu.SetActive(false);
        menuToLoad.SetActive(true);
    }
}
