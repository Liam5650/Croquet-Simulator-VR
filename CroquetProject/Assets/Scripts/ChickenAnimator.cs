using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{

    public float idleWaitTime = 2.5f;   // Time to wait while idling
    public float idleAngle = 30;        // Angle to rotate after idling
    public float lookingTime = 0.25f;
    public float idleBoost = 40;        // Boost force from idle state
    public float hopBoost = 40;         // Jump force
    public float aboutFaceRotateTime = 1.5f;    // Time to rotate 180 degrees after bumping into something
    public float walkTime = 2f;         // Time to spend walking when moving

    private float timer = 0f;
    private float initAngle = 0f;
    private float targetAngle = 0f;
    private float rotateTime = 0f;

    private string borderTag = "Border";    // Tag for objects that designate playing border

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
        timer = idleWaitTime - Random.value * idleWaitTime; // Random startup
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
                    targetAngle = initAngle + idleAngle * (Random.value * 2 - 1);   // Random within +/- range
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
                    rb.AddForce(transform.forward * idleBoost);
                    rb.AddForce(transform.up * hopBoost);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collide (with fence or solid prop) -> cancel force, aboutface

        if (activeState == State.Walking)
        {
            // If hit a border, turn right around
            if (collision.gameObject.tag == borderTag)
            {
                activeState = State.Looking;
                timer = 0;

                // Setup angles
                initAngle = transform.rotation.eulerAngles.y;
                targetAngle = initAngle + 180;  // Full turn
                rotateTime = aboutFaceRotateTime;
            }
        }
    }
}
