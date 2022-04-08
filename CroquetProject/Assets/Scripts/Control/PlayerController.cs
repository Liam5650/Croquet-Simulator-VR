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