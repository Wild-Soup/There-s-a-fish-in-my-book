using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerManager : MonoBehaviour
{
    public bool isHiding;
    public GameObject hidingObject;
    public InputActionReference hideExit;
    public float playerSpeed;
    public ActionBasedContinuousMoveProvider idk;
    public bool enteringHideout;

    private void OnEnable()
    {
        hideExit.action.Enable();

        hideExit.action.performed += exitHide;
    }

    public void exitHide(InputAction.CallbackContext ctx)
    {
        if (isHiding)
        {
            transform.position = hidingObject.GetComponent<Hide>().oldPos;
            isHiding = false;
            idk.moveSpeed = playerSpeed;
            GetComponent<CharacterController>().enabled = true;
        }
    }

    public void hide(GameObject a)
    {
        isHiding = true;
        hidingObject = a;
        idk.moveSpeed = 0;
        GetComponent<CharacterController>().enabled = false;
    }

}
