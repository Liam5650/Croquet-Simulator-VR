using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public Transform avatar; //Parent object transform for all avatar objects (headset, controllers)
    public GameObject leftController, rightController; //Controller Gameobjects

    private VRControls vrControls;

    private void Awake()
    {
        instance = this;
        vrControls = new VRControls();
    }

    //Enable the VR control scheme
    private void OnEnable()
    {
        vrControls.Enable();
    }

    //Disable the VR control scheme
    private void OnDisable()
    {
        vrControls.Disable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Handle Laser
        if(vrControls.VRPlayer.Teleport.WasPressedThisFrame() == true)
        {
            LaserController.instance.shoot();
        }
        if (vrControls.VRPlayer.Teleport.WasReleasedThisFrame() == true)
        {
            LaserController.instance.stopshoot();
        }
    }

}