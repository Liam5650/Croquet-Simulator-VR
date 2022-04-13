using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public Transform avatar; //Parent object transform for all avatar objects (headset, controllers)
    public GameObject leftController, rightController, headset; //VR Gameobjects

    private VRControls vrControls;
    private bool leftHolding = false, rightHolding = false;

    public GameObject nonVRInput;
    public MonoBehaviour headsetTracking, leftControllerTracking, rightControllerTracking;
    public bool ToggleVR;

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

    // Update is called once per frame
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

        //Hand COntrols
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