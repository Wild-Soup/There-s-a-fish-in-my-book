using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    private float orignalFOV;
    private Camera cm;
    public float shaekAmount;


    private void Start()
    {
        cm = Camera.main;
        orignalFOV = cm.fieldOfView;
    }
    // Update is called once per frame
    void Update()
    {
        float rn = Random.Range(-1 * shaekAmount, shaekAmount);
        cm.fieldOfView = orignalFOV + rn;
    }
}
