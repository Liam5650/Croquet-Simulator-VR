using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenController : MonoBehaviour
{
    // BlackScreenController - controls the screen fading behavior as used in teleports and level transitions

    public static BlackScreenController instance; // We want this class to be accessible from any other script 
    public Material blackScreen; // The material that we change the alpha of to achieve the blackscreen effect
    private bool fadingTo, fadingFrom; // Keeps track of where we are in the fade process
    private float fadeSpeed = 1, waitTime; // User-changeable speed in which to fade black in and out, as well as the wait time at full black
    
    // Set Instance
    private void Awake()
    {
        instance = this;
    }

    // We start the game fading from a full black screen
    void Start()
    {
        fadingFrom = true;
        waitTime = 0.5f;
        blackScreen.color = new Color(0f,0f,0f,1f);
    }

    // Update tracks where we are if a fade is initiated
    void Update()

        // This wait time allows elements or scenes to load if necessary
    {   if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        // Start the transition
        else
        {

            // Fade the blackscreen material in, ie the alpha is 1
            if (fadingTo)
            {
                if (blackScreen.color.a < 1f)
                {
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
                }
            }

            // Fade the blackscreen material out, ie alpha is 0
            else if (fadingFrom)
            {
                if (blackScreen.color.a > 0f)
                {
                    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                }
            }
        }

    }

    // Initiates the fade in process
    public void FadeTo(float speed)
    {
        fadeSpeed = speed;
        fadingTo = true;
        fadingFrom = false;
    }

    // Initiates the fade out process
    public void FadeFrom(float speed)
    {
        fadeSpeed = speed;
        fadingTo = false;
        fadingFrom = true;
    }

    // Alpha check to see if either of the above processes are complete
    public float GetAlpha()
    {
        return blackScreen.color.a;
    }
}
