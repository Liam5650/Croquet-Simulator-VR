using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    private float timer;
    public float expandTime, expandScaler, dissapearHeight;
    public Rigidbody rb;
    private Vector3 scalar;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        scalar = new Vector3(expandScaler, expandScaler, expandScaler);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= expandTime)
        {
            gameObject.transform.localScale += (Time.deltaTime / expandTime) * scalar;
            gameObject.transform.Translate(0, (Time.deltaTime / expandTime) * scalar.y /5, 0);
        }
        else if (timer >= expandTime && transform.parent != null)
        {
            rb.isKinematic = false;
            gameObject.transform.SetParent(null);
        }

        if (gameObject.transform.position.y >= dissapearHeight)
        {
            Destroy(gameObject);
        }
    }

}
