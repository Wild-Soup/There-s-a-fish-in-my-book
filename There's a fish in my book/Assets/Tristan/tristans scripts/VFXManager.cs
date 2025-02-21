using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject VFX;
    public GameObject spawnedObject;
    public void OpenBookTrigger(Transform parent)
    {
        spawnedObject = Instantiate(VFX, parent);
    }

    public void CloseBookTrigger()
    {
        Destroy(spawnedObject);
    }

    public void SetVFX(GameObject vfx)
    {
        VFX = vfx;
    }
}
