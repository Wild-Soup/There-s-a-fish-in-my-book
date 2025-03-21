using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnmarkScript1 : MonoBehaviour
{
    public float lifetime;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, lifetime);
    }
}
