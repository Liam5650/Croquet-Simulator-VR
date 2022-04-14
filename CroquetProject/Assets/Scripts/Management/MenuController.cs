using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // MenuController - handles the menu input action from the PlayerController script. 

    public static MenuController instance; // We want this class to be accessible from any other script 
    public Transform anchorPoint; // References the point we want the menu anchored to, ie the player position
    public float distanceFromPlayer, height; // User-changeable options for the distance to spawn the menu, and height
    private bool isOpened = false; // Check for whether or not the menu is open

    AudioSource audioSource;

    // Set instance
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Handles action for the button that opens and closes the menu
    public void menuButtonPressed()
    {
        audioSource.time = 0.3f;
        audioSource.Play();

        // If its opened, we disable all children
        if (isOpened)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            isOpened = false;
        }

        // If it is closed, get the correct position to place it and enable the main menu 
        else if (!isOpened)
        {
            transform.position = anchorPoint.position + new Vector3(anchorPoint.forward.x, height, anchorPoint.forward.z).normalized * distanceFromPlayer;
            transform.rotation = Quaternion.Euler(0f, anchorPoint.rotation.eulerAngles.y, 0f);
            transform.GetChild(0).gameObject.SetActive(true);
            isOpened = true;
        }
    }
}
