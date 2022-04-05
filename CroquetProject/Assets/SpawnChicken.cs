using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChicken : MonoBehaviour
{
    public GameObject chicken;
    public float spawnDistanceOffset = 5;
    public int num = 5;

    public bool test = false;


    private void Update()
    {
        if (test)
        {
            SpawnChickenBatch();
            test = false;
        }
    }

    public void SpawnChickenBatch()
    {
        for (int i = 0; i< num; i++)
        {
            GameObject newChicken = Instantiate(chicken, transform.position, transform.rotation);
            newChicken.transform.Rotate(transform.up, i * (360 / num));
            newChicken.transform.Translate(newChicken.transform.forward * spawnDistanceOffset);
        }
    }
}
