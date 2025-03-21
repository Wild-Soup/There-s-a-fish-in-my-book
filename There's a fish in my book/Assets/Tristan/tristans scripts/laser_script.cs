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
    

    private void FixedUpdate()
    {
        time += Time.deltaTime / colorDuration;
        Color emissioncolor = laserColor.Evaluate(time % 1f);
        laserMaterial.SetColor("_EmissionColor", emissioncolor * laserEmision);

        float rnd = Random.Range(-laserFrequency, laserFrequency);

        cylinderLaser.localScale = new Vector3(laserThicknes + rnd, cylinderLaser.localScale.y, laserThicknes + rnd);

        if (isOn)
        {
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
            cylinderLaser.localScale = Vector3.Lerp(new Vector3(cylinderLaser.localScale.x, cylinderLaser.localScale.y, cylinderLaser.localScale.z),
                    new Vector3(cylinderLaser.localScale.x, 0, cylinderLaser.localScale.z),
                    bladeSpeed);
            cylinderLaser.localPosition = Vector3.Lerp(new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y, cylinderLaser.localPosition.z),
                new Vector3(cylinderLaser.localPosition.x, cylinderLaser.localPosition.y,0 ), bladeSpeed);
        }
    }

    public void SwitchSaber()
    {
        isOn = !isOn;
        
    }

}


