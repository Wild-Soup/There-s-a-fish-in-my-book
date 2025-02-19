using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishscript : MonoBehaviour
{
    public HingeJoint tailJoint;
    public float swimForce = 5f;
    public float swimSpeed = 3f;

    void Start()
    {
        if (tailJoint == null) tailJoint = GetComponent<HingeJoint>();
    }

    void FixedUpdate()
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
