using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracker : MonoBehaviour
{
    public int ballIndex = 0; // 0 or 1, depending on which ball this is
    public int nextGate = 1;
    public GameObject scoreEffect;
    public float effectOffset;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    internal void ScoreGate()
    {
        nextGate++;
        StatManager.manager.UpdateCurrentGate(ballIndex, nextGate);
        Instantiate(scoreEffect, new Vector3(transform.position.x, transform.position.y + effectOffset, transform.position.z), transform.rotation);
        scoreEffect.GetComponent<AudioSource>().Play();
    }

    internal void UnscoreGate()
    {
        nextGate--;
        StatManager.manager.UpdateCurrentGate(ballIndex, nextGate);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Generic hit sound (mallet has own hit sound)
        if (!collision.gameObject.GetComponentInParent<CroquetMallet>())
        {
            audioSource.Play();
        }

        // Hit a ball?
        if (collision.gameObject.GetComponent<BallTracker>())
        {
            // Trigger a roquet
            StatManager.manager.RoquetStroke();
        }


    }
}
