using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBreak : MonoBehaviour
{
    public ParticleSystem Smoke;
    public GameObject pot;
    public GameObject brokenPot;
    public AudioSource sound;
    [Tooltip("The speed at wich the pot breaks")]
    public float breakPoint;
    private Rigidbody rb;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;
    }

    public void BreakPot()
    {
        pot.GetComponent<MeshCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
        pot.GetComponent<MeshRenderer>().enabled = false;
        brokenPot.SetActive(true);
        if (Smoke != null)
            Smoke.Play();
        if(sound != null)
            sound.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (velocity > breakPoint || collision.rigidbody.velocity.magnitude > breakPoint)
        {
            BreakPot();
        }
    }
}
