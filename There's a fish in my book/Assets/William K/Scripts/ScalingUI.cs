using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class ScalingUI : MonoBehaviour
{
    public GameObject sliderOBJ;
    public GameObject settingOBJPos;
    public GameObject cam;

    public InputActionReference pressAction;
    public bool openMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnEnable()
    {
        pressAction.action.Enable();

        pressAction.action.performed += OnPress;
    }

    public void OnDisable()
    {
        pressAction.action.Disable();

        pressAction.action.performed -= OnPress;
    }

    public void OnPress(InputAction.CallbackContext ctx)
    {
        if (!openMenu && !GetComponentInParent<PlayerManager>().isHiding)
        {
            openMenu = true;
            sliderOBJ.SetActive(true);
            sliderOBJ.transform.rotation = cam.transform.rotation;
            sliderOBJ.transform.position = settingOBJPos.transform.position;
        }
        else
        {
            openMenu = false;
            sliderOBJ.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
