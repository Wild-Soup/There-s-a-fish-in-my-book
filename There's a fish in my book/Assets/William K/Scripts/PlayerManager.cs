using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool isHiding;
    public GameObject hidingObject;
    public InputActionReference hideExit;
    public float playerSpeed;
    public ActionBasedContinuousMoveProvider idk;
    public bool enteringHideout;
    public GameManager gameManager;

    private void Start()
    {

    }

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
            GetComponent<XROrigin>().CameraYOffset = GetComponentInChildren<Scaling>().slider.GetComponent<Slider>().value-0.7f;
        }
    }

    public void hide(GameObject a)
    {
        isHiding = true;
        hidingObject = a;
        idk.moveSpeed = 0;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<XROrigin>().CameraYOffset = -0.2f;
    }

}
