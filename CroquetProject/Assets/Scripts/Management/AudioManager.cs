using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip[] sfx;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(int sfxIndex)
    {
        audioSource.PlayOneShot(sfx[sfxIndex]);
    }

}
