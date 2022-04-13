using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGZone : MonoBehaviour
{
    // ZeroGZone - creates an area where forces are applied to rigidbodies

    public float force; // The force to place on objects 
    public Transform direction; // The direction the force is applied

    // If an object goes into the trigger zone, apply the force
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            if(other.tag == "Pickup")
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(direction.forward*force);
            } 
        }
    }
}
