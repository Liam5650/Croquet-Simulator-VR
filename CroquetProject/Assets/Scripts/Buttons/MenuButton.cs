using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ButtonController
{
    // MenuButton - used for buttons that navigate to other sub-menus in the main menu

    private GameObject currentMenu; // Reference for the current menu open
    public GameObject menuToLoad; // Reference for the menu we want to switch to 

    //Perform button press execution funtion, for this it switches the menu
    public override void Execute()
    {
        currentMenu = transform.parent.gameObject;
        currentMenu.SetActive(false);
        menuToLoad.SetActive(true);
    }
}
