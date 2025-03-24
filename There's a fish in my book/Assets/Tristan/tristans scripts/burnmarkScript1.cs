using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class burnmarkScript1 : MonoBehaviour
{
    public float lifetime;
    private DecalProjector decal;

    private void Start()
    {
        decal = GetComponent<DecalProjector>();
    }
    // Update is called once per frame
    void Update()
    {
        decal.fadeFactor = decal.fadeFactor - (lifetime * Time.deltaTime);

        if (decal.fadeFactor <= 0)
            Destroy(this.gameObject);
    }
}
