using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{

    public float idleWaitTime = 2.5f;   // Time to wait while idling
    public float idleAngle = 30;        // Angle to rotate after idling
    public float lookingTime = 0.25f;
    public float idleBoost = 40;        // Boost force from idle state
    public float aboutfaceRotateTime = 1.5f;    // Time to rotate 180 degrees after bumping into something
    public float walkTime = 2f;         // Time to spend walking when moving

    private float timer = 0f;
    private float initAngle = 0f;
    private float targetAngle = 0f;
    private float rotateTime = 0f;

    private Rigidbody rb;

    enum State
    {
        Idle,
        Looking,
        Walking
    }

    State activeState = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        switch (activeState)
        {
            case State.Idle:
                if (timer > idleWaitTime)
                {
                    // Change of State
                    activeState = State.Looking;
                    timer = 0;

                    // Setup angles
                    initAngle = transform.rotation.eulerAngles.y;
                    targetAngle = initAngle + idleAngle;
                    rotateTime = lookingTime;
                }
                break;

            case State.Looking:
                // Find new angle via lerp
                float newAngle = Mathf.LerpAngle(initAngle, targetAngle, Mathf.Min(timer, lookingTime) / lookingTime);
                // Prepare associated quaternion
                Quaternion newRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, newAngle, transform.rotation.eulerAngles.z));
                // Rotate accordingly
                transform.SetPositionAndRotation(transform.position, newRotation);

                if (timer > rotateTime)
                {
                    // Change of State
                    activeState = State.Walking;
                    timer = 0;

                    // Add force
                    rb.AddForce(Vector3.forward * idleBoost);
                }
                break;

            case State.Walking:

                if (timer > walkTime)
                {
                    // Change of State
                    activeState = State.Idle;
                    timer = 0;

                    // Clear force
                    rb.velocity = Vector3.zero;
                }
                break;
        }

        // Walking
        // wait a period -> cancel contstant forward force, enter idle
        
        
        // Aboutface
        // rotate 180 over time -> Idle
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collide (with fence or solid prop) -> cancel force, aboutface

        if (activeState == State.Walking)
        {
            // Check other
            // STUB
        }
    }
}
