using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public static LaserController instance;

    private LineRenderer laserLine; //The line to be rendered to indicate the laser
    public Transform shotPoint; //The point at which the laser begins from
    public float laserRange; //The distance to which to check if the laser hits something
    private bool shooting; //Indicates for the update loop whether or not the laser is currently firing
    public Transform spawnPoint; //The spawn point that will be set to move the player to
    public GameObject spawnPointObject; //The circle on the ground indicating to the player where they will spawn
    public bool spawnSet = false; //Indicates if a spawn point has been set for the teleport controller
    private GameObject button = null; //Reference for a button if the laser hits one

    private void Awake()
    {
        instance = this;
    }

    // Get the laser line renderer component and set default state to not shooting
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        shooting = false;
    }

    // Check to see if the laser is firing, and then carries out functionality
    void Update()
    {
        if (shooting)
        {
            // Set laser origin point
            laserLine.SetPosition(0, shotPoint.position);

            //Fire ray, check for hit
            RaycastHit hit;
            if (Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, laserRange))
            {
                // Set laser end point to hit point
                laserLine.SetPosition(1, hit.point);

                //If hitting ground, set spawn and unset button if there is one highlighted
                if (hit.collider.tag == "Ground")
                {
                    if (button != null)
                    {
                        button.transform.GetChild(1).gameObject.SetActive(false);
                        button = null;
                    }
                    spawnPointObject.transform.position = hit.point;
                    spawnPointObject.SetActive(true);
                    spawnPointObject.GetComponent<Renderer>().material.color = Color.blue;
                    laserLine.GetComponent<Renderer>().material.color = Color.blue;
                    spawnPoint.position = hit.point;
                    spawnSet = true;
                }
                //If we hit a button and not already hitting another, store button reference and highlight
                else if (hit.collider.tag == "Button" && button == null)
                {
                    button = hit.collider.gameObject;
                    button.transform.GetChild(1).gameObject.SetActive(true);
                    spawnPointObject.SetActive(false);
                    laserLine.GetComponent<Renderer>().material.color = Color.green;
                    spawnSet = false;
                }
                //If we hit a button on the prev frame and hit another, unselect old and update reference to new
                else if (hit.collider.tag == "Button" && button != null)
                {
                    button.transform.GetChild(1).gameObject.SetActive(false);
                    button = hit.collider.gameObject;
                    button.transform.GetChild(1).gameObject.SetActive(true);
                    spawnPointObject.SetActive(false);
                    laserLine.GetComponent<Renderer>().material.color = Color.green;
                    spawnSet = false;
                }

                //If hitting something not specified, unset spawn and button
                else
                {
                    if (button != null)
                    {
                        button.transform.GetChild(1).gameObject.SetActive(false);
                        button = null;
                    }
                    spawnPointObject.SetActive(false);
                    spawnPointObject.GetComponent<Renderer>().material.color = Color.red;
                    laserLine.GetComponent<Renderer>().material.color = Color.red;
                    spawnSet = false;
                }
            }

            //If no hit, unset spawn and set laser end point forward from origin, and unset button
            else
            {
                if (button != null)
                {
                    button.transform.GetChild(1).gameObject.SetActive(false);
                    button = null;
                }
                spawnPointObject.SetActive(false);
                spawnPointObject.GetComponent<Renderer>().material.color = Color.red;
                laserLine.GetComponent<Renderer>().material.color = Color.red;
                spawnSet = false;
                laserLine.SetPosition(1, (laserRange * shotPoint.forward) + shotPoint.position);
            }
        }
    }

    //Fire the laser
    public void shoot()
    {
        laserLine.enabled = true;
        shooting = true;
    }

    //Stop firing the laser
    public void stopshoot()
    {
        laserLine.enabled = false;
        shooting = false;
        spawnPointObject.SetActive(false);

        //If the spawn is set, we call the TeleportController to handle teleporting
        if (spawnSet)
        {
            TeleportController.instance.StartFade();
            spawnSet = false;
        }
        //If the button has been set, execute it
        else if (button != null)
        {
            var buttonScript = button.GetComponent<ButtonController>();
            buttonScript.Execute();
        }
    }
}