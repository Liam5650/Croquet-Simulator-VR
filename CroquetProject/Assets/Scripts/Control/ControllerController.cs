using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerController : MonoBehaviour
{
    private bool pickingUp = false;
    private GameObject reference;
    private Vector3 currentPosition, velocity;
    public float breakForce;

    private void Start()
    {
        currentPosition = gameObject.transform.position;
    }

    private void Update()
    {
        velocity = (transform.position - currentPosition) / Time.deltaTime;
        currentPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pickup" && pickingUp && gameObject.GetComponent<FixedJoint>() == null)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            gameObject.GetComponent<FixedJoint>().breakForce = breakForce;
        }
    }

    public void PickUpOrDrop()
    {
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            pickingUp = true;
        }
        else if (gameObject.GetComponent<FixedJoint>() != null)
        {
            reference = gameObject.GetComponent<FixedJoint>().connectedBody.gameObject;
            Destroy(gameObject.GetComponent<FixedJoint>());
            reference.GetComponent<Rigidbody>().velocity = velocity;
            reference = null;
        }
    }

    public void StopPickupOrDrop()
    {
        pickingUp = false;
    }
}
