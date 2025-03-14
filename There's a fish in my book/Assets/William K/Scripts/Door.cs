using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public GameManager gameManger;
    public GameObject txt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!gameManger.EndDay())
            {
                txt.SetActive(true);
                StartCoroutine(DisableText());
            }
        }
    }


    public IEnumerator DisableText()
    {
        yield return new WaitForSeconds(5);
        txt.SetActive(false);
    }
}
