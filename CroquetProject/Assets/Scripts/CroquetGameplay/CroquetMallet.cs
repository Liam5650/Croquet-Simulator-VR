using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroquetMallet : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void MalletHit()
    {
        audioSource.Play();
        StatManager.manager.AddStroke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Hit a ball?
        if (collision.gameObject.GetComponent<BallTracker>())
        {
            MalletHit();
        }
    }
}
