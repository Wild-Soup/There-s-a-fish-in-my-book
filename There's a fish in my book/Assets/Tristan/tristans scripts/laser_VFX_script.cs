using UnityEngine;

public class laser_VFX_script : MonoBehaviour
{
    [Header("Laser")]
    public Transform cylinderLaser;
    public Material laserMaterial;
    public Gradient laserColor;
    public float colorDuration = 5f;
    private float time;
    public float laserEmision;
    public float laserThicknes =1;
    public float laserFrequency = 1;
    public float bladelength;
    public float bladeSpeed;
    public bool isOn;
    public bool isDuel;
    private bool swinging = false;

    private Rigidbody rb;

    [Header("SFX")]
    public AudioSource turnOnSound;
    public AudioSource turnOffSound;
    public AudioSource idleSound;
    public AudioSource swingSound;

    [Tooltip("How fast the object needs to move in order to qualify for making the swing noise. [0] is velocity, [1] is angular velocity.")]
    [SerializeField] private float[] swingThreshold = new float[2];

    [Tooltip("The rate at which velocity changes the pitch of the idle noise. [0] is velocity, [1] is angular velocity.")]
    [SerializeField] private float[] pitchRate = new float[2];

    [Tooltip("A constant that is added at the end of the pitch rate calculations.")]
    [SerializeField] private float pitchConstant = 0.8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Sets color
        time += Time.deltaTime / colorDuration;
        Color emissioncolor = laserColor.Evaluate(time % 1f);
        laserMaterial.SetColor("_EmissionColor", emissioncolor * laserEmision);

        // Flicker
        float rnd = Random.Range(-laserFrequency, laserFrequency);
        cylinderLaser.localScale = new Vector3(laserThicknes + rnd, cylinderLaser.localScale.y, laserThicknes + rnd);

        if (isOn)
        {
            idleSound.volume = Mathf.Lerp(idleSound.volume, 1, bladeSpeed);

            // Single blade
            if (!isDuel)
            {
                // expand
                cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                new Vector3(cylinderLaser.localScale.x, bladelength, cylinderLaser.localScale.z),
                bladeSpeed);
                cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                    new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, bladelength), bladeSpeed);
            }

            // Duel blade
            else
            {
                // expand
                cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                new Vector3(cylinderLaser.localScale.x, bladelength * 2, cylinderLaser.localScale.z),
                bladeSpeed);
                cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                    new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, 0), bladeSpeed);
            }
        }

        // Decrease
        else
        {
            idleSound.volume = Mathf.Lerp(idleSound.volume, 0, bladeSpeed);
            cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                    new Vector3(cylinderLaser.localScale.x, 0, cylinderLaser.localScale.z),
                    bladeSpeed);
            cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y,0 ), bladeSpeed);
        }

        // Idle pitch
        idleSound.pitch = Mathf.Lerp(idleSound.pitch,
            Mathf.Clamp(rb.velocity.magnitude / pitchRate[0] + rb.angularVelocity.magnitude / pitchRate[1] + pitchConstant * Time.deltaTime, 1, 3),
            0.25f);

        // Velocity based sounds
        if (rb.velocity.magnitude > swingThreshold[0] && swinging == false)
        {
            swingSound.Play();

            swinging = true;
        }
        else
        {
            swinging = false;
        }

        // nagular veloc
        if (rb.angularVelocity.magnitude > swingThreshold[1] && swinging == false)
        {
            swingSound.Play();

            swinging = true;
        }
        else
        {
            swinging = false;
        }
    }

    public void SwitchSaber()
    {
        isOn = !isOn;

        if (isOn)
            turnOnSound.Play();
        else
            turnOffSound.Play();
        
    }

    public void TurnOf()
    {
        if (isOn)
        {
            isOn = false;
            turnOffSound.Play();
        }
    }

}


