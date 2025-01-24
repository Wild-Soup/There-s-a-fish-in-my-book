using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputHandler : MonoBehaviour
{
    private InputDevice leftController, rightController;

    void Start()
    {
        // H�mta de enheter som motsvarar de v�nstra och h�gra kontrollerna
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller, inputDevices);

        foreach (var device in inputDevices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                leftController = device;
            }
            else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            {
                rightController = device;
            }
        }
    }

    void Update()
    {
        // Kolla om knappen �r nedtryckt p� v�nster kontroll
        if (leftController.isValid && leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        {
            Debug.Log("V�nster knapp trycks p�.");
        }

        // Kolla om knappen �r nedtryckt p� h�ger kontroll
        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressedRight) && isPressedRight)
        {
            Debug.Log("H�ger knapp trycks p�.");
        }
    }
}
