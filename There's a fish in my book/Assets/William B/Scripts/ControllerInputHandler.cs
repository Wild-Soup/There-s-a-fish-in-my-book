using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerInputHandler : MonoBehaviour
{
    private InputDevice leftController, rightController;

    void Start()
    {
        // Hämta de enheter som motsvarar de vänstra och högra kontrollerna
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
        // Kolla om knappen är nedtryckt på vänster kontroll
        if (leftController.isValid && leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        {
            Debug.Log("Vänster knapp trycks på.");
        }

        // Kolla om knappen är nedtryckt på höger kontroll
        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressedRight) && isPressedRight)
        {
            Debug.Log("Höger knapp trycks på.");
        }
    }
}
