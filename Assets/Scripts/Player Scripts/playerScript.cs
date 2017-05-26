using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : Controller {
    public String xInput = "Vertical";
    public String zInput = "Horizontal";
    public KeyCode fireButton = KeyCode.Space;
    //misc variables
    Rigidbody playerRB;
    float timer;

    public int player = 1;

    //movement variables
    public float turnSpeed = 5;
    public float speed = 20;
    //health variables

    //weapon variable
    public int weaponType; //1 = gun
    public int recoil = 100;
    public double weaponShootSpeed = 0.1;

    float playerForward;
    float turnInput;

    void Awake() {
        die();
    }

    void Update() {

        playerForward = Input.GetAxisRaw(xInput);
        turnInput = Input.GetAxis(zInput);
        timer += Time.deltaTime;

        if (Input.GetKey(fireButton)) {
            if (timer >= weaponShootSpeed) {
                timer = 0;
                shootWeapon(player, weaponType, recoil);
            }
        }
    }

    void FixedUpdate() {
        updateForwardAndRotation(playerForward * speed, turnInput * turnSpeed);
    }
}