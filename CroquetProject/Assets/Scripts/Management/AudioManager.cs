using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // AudioManager - class that allows sound effects and level music to be played by other scripts

    public static AudioManager instance; // We want this class to be accessible from any other script 
    AudioSource audioSource; // Audiosource component to play clips
    public AudioClip[] sfx; // List of audioclips that can be played

    // Set instance
    private void Awake()
    {
        instance = this;
    }

    // Get the audiosource component set up
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Play a sound effect
    public void PlaySFX(int sfxIndex)
    {
        audioSource.PlayOneShot(sfx[sfxIndex]);
    }

}
