using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject VFX;
    public GameObject spawnedObject;
    public void OpenBookTrigger(Transform parent)
    {
        if (VFX != null)
        {
            Debug.Log("dunnno");
            spawnedObject = Instantiate(VFX, parent);

            Transform lib = GameObject.FindAnyObjectByType<LibrarianAI>().transform;

            float distance = Vector3.Distance(transform.position, lib.position);

            lib.GetComponent<LibrarianAI>().IncreaseAnger(50f);
        }
    }

    public void CloseBookTrigger()
    {
        if (spawnedObject != null)
            Destroy(spawnedObject);
    }

    public void SetVFX(GameObject vfx)
    {
        VFX = vfx;
    }
}
