using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Vector3 oldPos;
   // public Transform hidePos;
    public GameObject player;
    public Transform hidePos;
    public bool enteringHideOut;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void leaveHideSpot()
    {
        
        player.transform.position = oldPos;
        
    }

    public void hide(Transform hidePos)
    {
        this.hidePos = hidePos;
        player.GetComponent<PlayerManager>().enteringHideout = true;
        oldPos = player.transform.position;
        StartCoroutine(HideDelay());
    }
    
    public IEnumerator HideDelay()
    {
        yield return new WaitForSeconds(0.15f);

        player.GetComponent<PlayerManager>().enteringHideout = false;
        player.transform.position = hidePos.position;
    }

}
