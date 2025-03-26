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
    public CharacterController cont;
    public InputActionReference a;
    public float playerSpeed;
    public ActionBasedContinuousMoveProvider idk;
    public bool enteringHideout;
    public GameManager gameManager;
    public AudioSource walkSound;

    private void Start()
    {

    }


    private void OnEnable()
    {
        hideExit.action.Enable();

        hideExit.action.performed += exitHide;

        a.action.Enable();

        a.action.started += Move;
        a.action.canceled += StopMove;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        walkSound.Play();
    }

    public void StopMove(InputAction.CallbackContext ctx)
    {
        walkSound.Stop();
    }

    public void exitHide(InputAction.CallbackContext ctx)
    {
        if (isHiding)
        {
            transform.position = hidingObject.GetComponent<Hide>().oldPos;
            isHiding = false;
            idk.moveSpeed = playerSpeed;
            GetComponent<CharacterController>().enabled = true;
            GetComponent<XROrigin>().CameraYOffset = GetComponentInChildren<Scaling>().slider.GetComponent<Slider>().value-0.15f;
        }
    }

    public void hide(GameObject a)
    {
        isHiding = true;
        hidingObject = a;
        idk.moveSpeed = 0;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<XROrigin>().CameraYOffset = 0;
    }
}
