using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerModeToggle : MonoBehaviour
{
    public InputActionReference changeMode;
    public GameObject flashLight;
    public GameObject hand;
    public ControllerMode mode;
    private void Start()
    {
        if(mode == ControllerMode.Hand)
        {
            flashLight.SetActive(false);
            hand.SetActive(true);
        }
        else if(mode == ControllerMode.Flashlight)
        {
            hand.SetActive(false);
            flashLight.SetActive(true);
        }


        changeMode.action.Enable();

        changeMode.action.performed += ChangeMode;
    }

    public void ChangeMode(InputAction.CallbackContext ctx)
    {
        if(mode == ControllerMode.Hand)
        {
            hand.SetActive(false);
            flashLight.SetActive(true);
            mode = ControllerMode.Flashlight;

        }else if(mode == ControllerMode.Flashlight)
        {
            flashLight.SetActive(false);
            hand.SetActive(true);
            mode = ControllerMode.Hand;
        }
    }

}

public enum ControllerMode { Hand, Flashlight};
