using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR;

public class Item : MonoBehaviour
{
    // Variables


    // Attributes


    // References


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerInputHandler.leftController.isValid && ControllerInputHandler.leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed))
        {
            WhenPickedUp();
        }
    }

    private void WhenPickedUp()
    {

    }
}
