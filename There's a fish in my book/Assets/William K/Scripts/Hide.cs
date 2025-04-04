using System.Collections;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Vector3 oldPos;
   // public Transform hidePos;
    public GameObject player;
    public Transform hidePos;
    public bool enteringHideOut;

    public GameObject hideVignette;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void leaveHideSpot()
    {
        hideVignette.GetComponent<Animator>().SetTrigger("hide");
        
        StartCoroutine(LeaveDelay());
        
    }

    public void hide(Transform hidePos)
    {
        if (!player.GetComponent<PlayerManager>().isHiding && !player.GetComponent<PlayerManager>().enteringHideout)
        {
            hideVignette.GetComponent<Animator>().SetTrigger("hide");
            this.hidePos = hidePos;
            player.GetComponent<PlayerManager>().enteringHideout = true;
            oldPos = player.transform.position;
            StartCoroutine(HideDelay());
        }
    }
    
    public IEnumerator HideDelay()
    {
        yield return new WaitForSeconds(0.6f);

        player.GetComponent<PlayerManager>().enteringHideout = false;
        player.transform.position = hidePos.position;
    }
    public IEnumerator LeaveDelay()
    {
        yield return new WaitForSeconds(0.6f);

        player.transform.position = oldPos;
    }

}
