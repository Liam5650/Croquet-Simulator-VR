using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPrevention : MonoBehaviour
{
    // Prevents objects from falling through the world by sending them above the ground plane.
    // Assumes a flat surface
    public float catchDistance = 5;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    { 
        if (transform.position.y < -1 * catchDistance)
        {
            rb.velocity = Vector3.zero;     // Remove velocity factors
            rb.angularVelocity = Vector3.zero;

            // Appear at last position but inverted y
            transform.position = new Vector3(   
                transform.position.x,
                transform.position.y * -1f,
                transform.position.z);

        }
    }
}
