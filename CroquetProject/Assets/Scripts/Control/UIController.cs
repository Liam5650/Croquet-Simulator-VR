using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image blackScreen;
    public float fadeSpeed, blackTime;
    private bool fadingTo, fadingBack, waiting;
    private float waited = 0f;

    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (fadingTo)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                fadingTo = false;
                PlayerController.instance.avatar.position = new Vector3(PlayerController.instance.spawnPoint.position.x, PlayerController.instance.avatar.position.y, PlayerController.instance.spawnPoint.position.z);
                waiting = true;
            }
        }
        else if (waiting)
        {
            if (waited <= blackTime)
            {
                waited += Time.deltaTime;
            }
            else
            {
                waiting = false;
                waited = 0f;
                fadingBack = true;
            }
        }
        else if (fadingBack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadingBack = false;
            }
        }
    }

    public void StartFade()
    {
        fadingTo = true;
    }
}
