using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGZone : MonoBehaviour
{
    public float force;
    public Transform direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(direction.forward);
    }

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
