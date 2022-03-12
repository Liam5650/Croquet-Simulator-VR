using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    public static TeleportController instance;

    public Image blackScreen; // UI Image object that is completely black, serves as the fadescreen
    public float fadeSpeed, blackTime; //fadespeed is the speed at which the screen turns to and from black, black time is time spent at black
    private bool fadingTo, fadingBack, waiting; //Bool variables to control where we are in the teleport process
    private float waited = 0f; //Amount of time waited at the blackscreen to allow for loading, is the opposite of blackTime

    private void Awake()
    {
        instance = this;
    }

    // Check to see if we have initiated a teleport every frame
    void Update()
    {

        //If the start of the teleport process has been started by StartFade(), begin the teleport process
        if (fadingTo)
        {

            //Begin by fading the screen to black
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            //If the screen is black, complete the teleport
            if (blackScreen.color.a == 1f)
            {
                fadingTo = false;
                PlayerController.instance.avatar.position = new Vector3(LaserController.instance.spawnPoint.position.x, PlayerController.instance.avatar.position.y, LaserController.instance.spawnPoint.position.z);
                waiting = true;
            }
        }

        //Wait for a specified time while the screen is black to allow for loading, etc
        else if (waiting)
        {
            if (waited <= blackTime)
            {
                waited += Time.deltaTime;
            }

            //Once we are done waiting, start the transition of the screen back from black
            else
            {
                waiting = false;
                waited = 0f;
                fadingBack = true;
            }
        }

        //Transition the screen from black back to normal
        else if (fadingBack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadingBack = false;
            }
        }
    }

    //Initiates the spawning process to be carried out in the update loop
    public void StartFade()
    {
        fadingTo = true;
    }
}
