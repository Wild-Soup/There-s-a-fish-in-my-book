using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public List<GameObject> VFX;
    public void OpenBookTrigger(int index)
    {
        Instantiate(VFX[index]);
    }

    public void CloseBookTrigger(GameObject toDestroy)
    {
        Destroy(toDestroy);
    }
}
