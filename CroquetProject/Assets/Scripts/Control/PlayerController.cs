using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // PlayerController - handles all player input and directs it to their respective scripts to execute

    public static PlayerController instance; // We want this class to be accessible from any other script 

    public Transform avatar; //Parent object transform for all avatar objects (headset, controllers)
    public GameObject leftController, rightController, headset; //VR Gameobjects

    private VRControls vrControls; // Input action object for controls
    private bool leftHolding = false, rightHolding = false; // Check for if a hand is holding something

    public GameObject nonVRInput; // Reference for the non-vr input
    public MonoBehaviour headsetTracking, leftControllerTracking, rightControllerTracking; // Tracking scripts for VR
    public bool ToggleVR; // Allows VR to be toggled on or off

    // Set instance and enable/disable functionality based on VR toggle
    private void Awake()
    {
        instance = this;
        vrControls = new VRControls();

        if (ToggleVR)
        {
            nonVRInput.SetActive(false);
            headsetTracking.enabled = true;
            leftControllerTracking.enabled = true;
            rightControllerTracking.enabled = true;
            avatar.position = new Vector3(avatar.position.x, 0f, avatar.position.z);
            if(PlayerPrefs.HasKey("Offset"))
            {
                avatar.position = new Vector3(avatar.position.x, avatar.position.y + PlayerPrefs.GetFloat("Offset"), avatar.position.z);
            }
        }
        else
        {
            nonVRInput.SetActive(true);
            headsetTracking.enabled = false;
            leftControllerTracking.enabled = false;
            rightControllerTracking.enabled = false;
        }
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

    // Handle input
    void Update()
    {
        //Teleport Controls
        if(vrControls.VRPlayer.Teleport.WasPressedThisFrame() == true)
        {
            LaserController.instance.shoot();
        }
        if (vrControls.VRPlayer.Teleport.WasReleasedThisFrame() == true)
        {
            LaserController.instance.stopshoot();
        }

        //Menu Controls
        if(vrControls.VRPlayer.Menu.WasPressedThisFrame() == true)
        {
            MenuController.instance.menuButtonPressed();
        }

        //Hand Controls
        if (vrControls.VRPlayer.PickupLeft.WasPressedThisFrame() == true)
        {
            leftController.GetComponent<ControllerController>().PickUpOrDrop();
        }
        if (vrControls.VRPlayer.PickupLeft.WasReleasedThisFrame() == true)
        {
            leftController.GetComponent<ControllerController>().StopPickupOrDrop();
        }
        if (vrControls.VRPlayer.PickupRight.WasPressedThisFrame() == true)
        {
            rightController.GetComponent<ControllerController>().PickUpOrDrop();
        }
        if (vrControls.VRPlayer.PickupRight.WasReleasedThisFrame() == true)
        {
            rightController.GetComponent<ControllerController>().StopPickupOrDrop();
        }
    }
}