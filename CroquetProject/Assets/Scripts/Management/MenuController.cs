using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public Transform anchorPoint;
    public float distanceFromPlayer, height;
    private bool isOpened = false;

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }


    public void menuButtonPressed()
    {
        audioSource.time = 0.3f;
        audioSource.Play();
        if (isOpened)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            isOpened = false;
        }
        else if (!isOpened)
        {
            transform.position = anchorPoint.position + new Vector3(anchorPoint.forward.x, height, anchorPoint.forward.z).normalized * distanceFromPlayer;
            transform.rotation = Quaternion.Euler(0f, anchorPoint.rotation.eulerAngles.y, 0f);
            transform.GetChild(0).gameObject.SetActive(true);
            isOpened = true;
        }
    }
}
