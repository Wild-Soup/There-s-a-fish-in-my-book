using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIntensityController : MonoBehaviour
{
    // Attributes
    [Tooltip("How fast the object needs to move to reach maxmimum intensity when it hits the ground.")]
    [SerializeField] private float maxVelocity = 10f;

    // Variables
    private float intensity, velocity, prevVelocity;

    // References
    private Rigidbody rb;
    private AudioSource source;

    [Tooltip("")]
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
        intensity = Mathf.Clamp(prevVelocity / maxVelocity,0.1f,1); //  the potental intensity increases with velocity until it reaches the max velocity

    }

    // Plays the sound on collision
    private void OnCollisionEnter(Collision collision)
    {
        // Only play sounds when there are sounds
        if (source.clip)
        {
            source.pitch = Mathf.Clamp(Mathf.Log(intensity * 10,10),0.7f,1);
            source.volume = intensity;
            source.PlayOneShot(sounds[Random.Range(0, sounds.Count - 1)]);
        }
        Debug.Log("velocity: " + prevVelocity + ", intensity: " + intensity);
    }
}
