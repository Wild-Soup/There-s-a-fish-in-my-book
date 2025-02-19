using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIntensityController : MonoBehaviour
{
    // Attributes
    [Tooltip("How fast the object needs to move to reach maxmimum loudness when it hits the ground.")]
    [SerializeField] private float maxVelocity = 10f;

    // Variables
    private float loudness, velocity, prevVelocity;

    // References
    private Rigidbody rb;
    private AudioSource source;

    [Tooltip("First clip is used for small collisions. Second for medium sized speed. Third for fast collisions.")]
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        prevVelocity = velocity;
        velocity = rb.velocity.magnitude;
        loudness = Mathf.Clamp(prevVelocity / maxVelocity,0.1f,1); //  the potental loudness increases with velocity until it reaches the max velocity

    }

    // Plays the sound on collision
    private void OnCollisionEnter(Collision collision)
    {
        //// Plays different sound based on the intensity
        //// Low intensity
        if (loudness >= 0 && loudness < 0.333f)
        {
            source.PlayOneShot(sounds[0], loudness);
        }

        //// Medium intensity
        else if (loudness >= 0.333f && loudness < 0.667f)
        {
            source.PlayOneShot(sounds[1], loudness);
        }

        //// High intensity
        else if (loudness >= 0.667f && loudness <= 1)
        {
            source.PlayOneShot(sounds[2], loudness);
        }
        Debug.Log("velocity: " + prevVelocity + ", loudness: " + loudness);
    }
}
