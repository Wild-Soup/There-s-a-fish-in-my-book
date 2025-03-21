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
    [SerializeField]private AudioSource source;

    [Tooltip("")]
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();

    [Tooltip("")]
    [SerializeField] private AnimationCurve pitchCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //source = GetComponent<AudioSource>();
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
        if (sounds.Count > 0)
        {
            // temp variables
            float iIntensity = pitchCurve.Evaluate(intensity);
            Debug.Log("better tensity: " + iIntensity);

            source.pitch = iIntensity;
            source.volume = intensity;
            source.PlayOneShot(sounds[Random.Range(0, sounds.Count - 1)]);

            // Librarian interactions
            if (GameObject.FindAnyObjectByType<LibrarianAI>() == null)
            {
                Transform lib = GameObject.FindAnyObjectByType<LibrarianAI>().transform;


                float distance = Vector3.Distance(transform.position, lib.position);

                lib.GetComponent<LibrarianAI>().IncreaseAnger(iIntensity * intensity * (1 / distance));
            }

        }
        Debug.Log("velocity: " + prevVelocity + ", intensity: " + intensity);
    }
}
