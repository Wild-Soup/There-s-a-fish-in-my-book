using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject VFX;
    public GameObject spawnedObject;

    private bool hasSpawned;
    public void OpenBookTrigger(Transform parent)
    {
        if (VFX != null && !hasSpawned)
        {
            Debug.Log("dunnno");
            spawnedObject = Instantiate(VFX, parent);

            float distance = Vector3.Distance(transform.position, GameManager.librarian.transform.position);

            GameManager.librarian.GetComponent<LibrarianAI>().IncreaseAnger(50f);

            if (spawnedObject.CompareTag("nonRemove"))
                hasSpawned = true;
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
