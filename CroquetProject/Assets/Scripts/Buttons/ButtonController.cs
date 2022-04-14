using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // ButtonController - acts as a superclass for the rest of the button scripts. 
    // Allows other scripts to not have to have a reference for every class of button 
    // for their respective execution functionality, and instead can simply call the 
    // ButtonController execute() method which is then overridden by other button scripts

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Perform button press execution funtion, by default this just does nothing.
    // Is overridden by the other button subclasses 
    public virtual void Execute()
    {
        ;
    }
}
