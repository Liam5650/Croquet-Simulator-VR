using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public Transform anchorPoint;
    public float distanceFromPlayer, height;
    private bool isOpened = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            Debug.Log(anchorPoint.forward);
            transform.position = anchorPoint.position + new Vector3 (anchorPoint.forward.x, height, anchorPoint.forward.z).normalized * distanceFromPlayer;
            transform.rotation =  Quaternion.Euler(0f, anchorPoint.rotation.eulerAngles.y, 0f);
        }
    }

    public void menuButtonPressed()
    {
        if(isOpened)
        {
            isOpened = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isOpened = true;
        }
    }
}
