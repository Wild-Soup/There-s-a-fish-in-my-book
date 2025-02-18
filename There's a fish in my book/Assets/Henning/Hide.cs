using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Vector3 oldPos;
    public Transform hidePos;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void leaveHideSpot()
    {
        
        player.transform.position = oldPos;
        
    }

    public void hide()
    {
        oldPos = player.transform.position;
        player.transform.position = hidePos.position;
        
    }
    

}
