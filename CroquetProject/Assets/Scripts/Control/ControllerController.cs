using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerController : MonoBehaviour
{
    private bool pickingUp = false; //Indicates if the player has initiated picking something up
    private GameObject reference; //Reference to the picked up gameobject
    private Vector3 currentPosition, velocity; //Used to calculated the velocity of the object to maintain after joint break
    public float breakForce; //Breakforce of the joint between the controller and picked up object

    //Get the current position of the controller
    private void Start()
    {
        currentPosition = gameObject.transform.position;
    }

    //Calculate the velocity of the controller 
    private void Update()
    {
        velocity = (transform.position - currentPosition) / Time.deltaTime;
        currentPosition = transform.position;
       // transform.position = new Vector3 (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
    }

    //Pick up an object. Check to see if the object has the right tag, pickingUp has been flagged, and if there is no other object attached
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pickup" && pickingUp && gameObject.GetComponent<FixedJoint>() == null)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            gameObject.GetComponent<FixedJoint>().breakForce = breakForce;
        }
    }

    //Used to either initiate a pickup or initiate a drop, depending on the state
    public void PickUpOrDrop()
    {
        //If we arent already holding something, we can pick up
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            pickingUp = true;
        }

        //If we are already holding something, break the joint and match the volocity of the object to that of the controller
        else if (gameObject.GetComponent<FixedJoint>() != null)
        {
            reference = gameObject.GetComponent<FixedJoint>().connectedBody.gameObject;
            Destroy(gameObject.GetComponent<FixedJoint>());
            reference.GetComponent<Rigidbody>().velocity = velocity;
            reference = null;
        }
    }

    //When the button is released, this turns off the pickingUp indicator
    public void StopPickupOrDrop()
    {
        pickingUp = false;
    }
}
