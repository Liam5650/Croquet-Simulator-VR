using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Perform button press execution funtion
    public void execute()
    {
        int a = 1;
    }

    //Enable the highlighting outline of the button
    public void highlight()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    //Disable the highlighting outline of the button
    public void unhighlight()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
