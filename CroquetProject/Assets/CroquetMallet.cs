using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroquetMallet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void MalletHit()
    {
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
