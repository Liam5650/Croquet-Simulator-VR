using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierButton : ButtonController
{
    // ModifierButton - modifies the Y position of some object (in particular, the player height)

    public float modAmount; //Float value that we want to modify something by
    public GameObject modObject; //Reference to player etc, to modify height

    //Perform button press execution funtion, ie add modifer to height
    public override void Execute()
    {
        modObject.transform.position = modObject.transform.position + new Vector3(0f, modAmount, 0f);

        // Set a reference in playerprefs to keep the height consistent across levels
        if(PlayerPrefs.HasKey("Offset"))
        {
            float oldOffset = PlayerPrefs.GetFloat("Offset");
            PlayerPrefs.SetFloat("Offset", oldOffset + modAmount);
        }
        else
        {
            PlayerPrefs.SetFloat("Offset", modAmount);
        }  
    }
}
