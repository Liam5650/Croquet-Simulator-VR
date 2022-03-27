using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTime : MonoBehaviour
{
    public float rotSpeedX = 0, rotSpeedY = 5, rotSpeedZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotSpeedX * Time.deltaTime, rotSpeedY * Time.deltaTime, rotSpeedZ * Time.deltaTime);
    }
}
