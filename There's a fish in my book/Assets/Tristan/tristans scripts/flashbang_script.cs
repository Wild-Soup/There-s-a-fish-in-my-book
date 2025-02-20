using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class flashbang_script : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject flashBang_Panel;
    [SerializeField] private AudioSource flashBang_Sound;
    [SerializeField] private float range =1;
    private Camera main_Camera;

    // Start is called before the first frame update
    void Start()
    {
        main_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform.GetChild(0).GetComponent<Canvas>().worldCamera = main_Camera;
        OnFlashBang();
    }

    public void OnFlashBang()
    {
        //If player is in range then play the flashbang visual
        if (Vector3.Distance(main_Camera.gameObject.transform.position, gameObject.transform.position) <= range)
        {
            flashBang_Panel.GetComponent<Animator>().SetTrigger("flash");
        }
        // Play flashbang sound
        flashBang_Sound.Play();
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
