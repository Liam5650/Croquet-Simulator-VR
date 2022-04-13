using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    public static TeleportController instance;

    public float fadeSpeed, blackTime; //fadespeed is the speed at which the screen turns to and from black, black time is time spent at black
    private bool fadingTo, fadingBack, waiting; //Bool variables to control where we are in the teleport process
    private float waited = 0f; //Amount of time waited at the blackscreen to allow for loading, is the opposite of blackTime
    //private Vector3 offset;
    //private Vector3 headpos, lastHeadpos;
    private void Awake()
    {
        instance = this;
        //offset = new Vector3(0f, 0f, 0f);
       // lastHeadpos = new Vector3(0f, 0f, 0f);
    }

    // Check to see if we have initiated a teleport every frame
    void Update()
    {
        //headpos = PlayerController.instance.headset.transform.localPosition;

        //If the start of the teleport process has been started by StartFade(), begin the teleport process
        if (fadingTo)
        {
            //Begin by fading the screen to black
            BlackScreenController.instance.FadeTo(fadeSpeed);

            //If the screen is black, complete the teleport
            if (BlackScreenController.instance.GetAlpha() == 1f)
            {
                fadingTo = false;

                //offset = new Vector3(headpos.x - lastHeadpos.x, headpos.y, headpos.z - lastHeadpos.z);
                //lastHeadpos = headpos;
                //PlayerController.instance.avatar.position = new Vector3(LaserController.instance.spawnPoint.position.x-offset.x, PlayerController.instance.avatar.position.y, LaserController.instance.spawnPoint.position.z-offset.z);
                PlayerController.instance.avatar.position = new Vector3(LaserController.instance.spawnPoint.position.x - PlayerController.instance.headset.transform.localPosition.x, PlayerController.instance.avatar.position.y, LaserController.instance.spawnPoint.position.z - PlayerController.instance.headset.transform.localPosition.z);
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
            BlackScreenController.instance.FadeFrom(fadeSpeed);
            if (BlackScreenController.instance.GetAlpha() == 0f)
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
