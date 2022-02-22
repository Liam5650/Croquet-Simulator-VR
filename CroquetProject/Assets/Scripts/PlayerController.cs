using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private LineRenderer laserLine;
    public Transform shotPoint;
    public float laserRange;
    private bool shooting;

    public Transform spawnPoint;
    public GameObject spawnPointObject;
    public bool spawnSet = false;
    public Transform avatar;

    public GameObject leftController, rightController;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            laserLine.SetPosition(0, shotPoint.position);
            

            RaycastHit hit;

            if (Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, laserRange))
            {
                laserLine.SetPosition(1, hit.point);

                if (hit.collider.tag == "Ground")
                {
                    spawnPointObject.transform.position = hit.point;
                    spawnPointObject.SetActive(true);
                    spawnPointObject.GetComponent<Renderer>().material.color = Color.blue;
                    laserLine.GetComponent<Renderer>().material.color = Color.blue;
                    spawnPoint.position = hit.point;
                    spawnSet = true;
                }
                else
                {
                    spawnPointObject.SetActive(false);
                    spawnPointObject.GetComponent<Renderer>().material.color = Color.red;
                    laserLine.GetComponent<Renderer>().material.color = Color.red;
                    spawnSet = false;
                }
            }
            else
            {
                spawnPointObject.SetActive(false);
                spawnPointObject.GetComponent<Renderer>().material.color = Color.red;
                laserLine.GetComponent<Renderer>().material.color = Color.red;
                spawnSet = false;
                laserLine.SetPosition(1, (laserRange * shotPoint.forward) + shotPoint.position);
            }
        }
    }

    public void shoot()
    {
        laserLine.enabled = true;
        shooting = true;
    }

    public void stopshoot()
    {
        laserLine.enabled = false;
        shooting = false;
        spawnPointObject.SetActive(false);
        if (spawnSet)
        {
            UIController.instance.StartFade();
            spawnSet = false;
        }
    }
}

