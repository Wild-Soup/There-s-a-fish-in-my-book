using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class Scaling : MonoBehaviour
{
    public GameObject slider;
    public TextMeshProUGUI heightTxt;

    private void Start()
    {
        float height = 1.7f;
        slider.GetComponent<Slider>().value = height;
        GetComponentInParent<XROrigin>().CameraYOffset = height-0.15f;
        heightTxt.text = $"{System.Math.Round(height, 2)}m";
    }
    public void ChangeHeight()
    {
        float height = slider.GetComponent<Slider>().value;
        GetComponentInParent<XROrigin>().CameraYOffset = height-0.15f;
        heightTxt.text = $"{System.Math.Round(height,2)}m";
        
    }

}
