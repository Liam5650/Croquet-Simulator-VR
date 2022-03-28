using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalletCollider : MonoBehaviour
{
    public CroquetMallet mallet;

    // Start is called before the first frame update
    void Start()
    {
        mallet = GetComponentInParent<CroquetMallet>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Hit a ball?
        if (collision.gameObject.GetComponent<BallTracker>())
        {
            mallet.MalletHit();
        }
    }
}
