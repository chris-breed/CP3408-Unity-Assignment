using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    Rigidbody RB;

    float speed;
    float speedMultiplier;
    float turnSpeed;

    void Start() {

    }

    public void stats(Rigidbody baseRB, float baseSpeed, float baseSpeedMultiplier, float baseTurnSpeed) {
        RB = baseRB;
        speed = baseSpeed;
        speedMultiplier = baseSpeedMultiplier;
        turnSpeed = baseTurnSpeed;

    }

    public void updateForwardAndRotation(float forward, float turn) {
        //movement
        if (forward > 0) {
            RB.AddForce(transform.forward * (speed * speedMultiplier));
        }

        //rotation
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }
}
