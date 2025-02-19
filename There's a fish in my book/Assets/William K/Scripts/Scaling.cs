using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Scaling : MonoBehaviour
{
    public GameObject slider;
    public TextMeshProUGUI heightTxt;

    private void Start()
    {
        float height = 1.5f;
        slider.GetComponent<Slider>().value = height;
        heightTxt.text = $"{System.Math.Round(height, 2)}m";
    }
    public void ChangeHeight()
    {
        float height = slider.GetComponent<Slider>().value;
        GetComponentInParent<CharacterController>().height = height;
        heightTxt.text = $"{System.Math.Round(height,2)}m";
        
    }

}
