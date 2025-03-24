using UnityEngine;

public class laser_VFX_script : MonoBehaviour
{
    //Laser
    [Header("Laser")]
    public Transform cylinderLaser;
    public Material laserMaterial;
    public Gradient laserColor;
    public float colorDuration = 5f;
    private float time;
    public float laserEmision;
    public float laserThicknes =1;
    public float laserFrequency = 1;
    public float bladeLenght;
    public float bladeSpeed;
    public bool isOn;
    public bool isDuel;

    [Header("SFX")]
    public AudioSource turnOnSound;
    public AudioSource turnOffSound;
    public AudioSource idleSound;
    public AudioSource swingSound;

    private void FixedUpdate()
    {
        
        time += Time.deltaTime / colorDuration;
        Color emissioncolor = laserColor.Evaluate(time % 1f);
        laserMaterial.SetColor("_EmissionColor", emissioncolor * laserEmision);

        float rnd = Random.Range(-laserFrequency, laserFrequency);

        cylinderLaser.localScale = new Vector3(laserThicknes + rnd, cylinderLaser.localScale.y, laserThicknes + rnd);

        if (isOn)
        {
            idleSound.volume = Mathf.Lerp(idleSound.volume, 1, bladeSpeed);
            if (!isDuel)
            {
                cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                new Vector3(cylinderLaser.localScale.x, bladeLenght, cylinderLaser.localScale.z),
                bladeSpeed);
                cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                    new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, bladeLenght), bladeSpeed);
            }
            else
            {
                cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                new Vector3(cylinderLaser.localScale.x, bladeLenght * 2, cylinderLaser.localScale.z),
                bladeSpeed);
                cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                    new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, 0), bladeSpeed);
            }
        }
        else
        {
            idleSound.volume = Mathf.Lerp(idleSound.volume, 0, bladeSpeed);
            cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                    new Vector3(cylinderLaser.localScale.x, 0, cylinderLaser.localScale.z),
                    bladeSpeed);
            cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y,0 ), bladeSpeed);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchSaber();
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


