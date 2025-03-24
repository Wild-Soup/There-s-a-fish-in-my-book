using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnmarkScript : MonoBehaviour
{
    public GameObject burnMarkPrefab; // Assign a Projector prefab with the burn texture
    public float delay;
    private float delayTime;

    private void FixedUpdate()
    {
        delayTime -= Time.fixedDeltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(delayTime <= 0)
        {
            if (other.CompareTag("Surface")) // Make sure the object has this tag
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    GameObject burnMark = Instantiate(burnMarkPrefab, hit.point, Quaternion.LookRotation(transform.up));
                    burnMark.transform.position = transform.position;
                    delayTime = delay;
                }
            }
        }
        
    }
}
