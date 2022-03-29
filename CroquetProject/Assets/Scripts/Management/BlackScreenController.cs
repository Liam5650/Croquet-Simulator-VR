using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenController : MonoBehaviour
{
    public static BlackScreenController instance;
    public Material blackScreen;
    private bool fadingTo, fadingFrom;
    private float fadeSpeed = 1;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadingFrom = true;
        blackScreen.color = new Color(0f,0f,0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingTo)
        {
            if (blackScreen.color.a < 1f)
            {
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            }
        }
        else if (fadingFrom)
        {
            if (blackScreen.color.a > 0f)
            {
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            }
        }
    }

    public void FadeTo(float speed)
    {
        fadeSpeed = speed;
        fadingTo = true;
        fadingFrom = false;
    }

    public void FadeFrom(float speed)
    {
        fadeSpeed = speed;
        fadingTo = false;
        fadingFrom = true;
    }

    public float GetAlpha()
    {
        return blackScreen.color.a;
    }
}
