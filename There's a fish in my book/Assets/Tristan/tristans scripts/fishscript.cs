using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishscript : MonoBehaviour
{
    public HingeJoint tailJoint;
    public float swimForce = 5f;
    public float swimSpeed = 3f;
    public bool moves;
    public bool makeSound;
    public AudioSource[] sounds = new AudioSource[5];


    void Start()
    {
        if (tailJoint == null) tailJoint = GetComponent<HingeJoint>();
    }

    void FixedUpdate()
    {
        if (moves)
        {
            // Apply sinusoidal movement to the joint motor
            JointMotor motor = tailJoint.motor;
            motor.force = swimForce;
            motor.targetVelocity = Mathf.Sin(Time.time * swimSpeed) * 100;
            motor.freeSpin = false;
            tailJoint.motor = motor;
            tailJoint.useMotor = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (makeSound)
        {
            int rn = Random.Range(0, sounds.Length);
            sounds[rn].Play();
        }
    }
}
