﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1Script : Controller {

    //misc variables
    public Rigidbody playerRB;
    float timer;

    int player; //1

    //movement variables
    public float forwardSpeed;
    public float speedModifier;
    public float turnSpeed;

    //health variables
    public int health;

    //weapon variable
    public int weaponType; //1 = cannon
    public int weaponShootSpeed;
    public int weaponDamage =1 ;

    float player1Forward;
    float player1Rotate;

    private void Awake() {
        player = 1;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("grid position " + gridX + " " + gridZ);
        timer += Time.deltaTime;

		if(health <= 0) {
            die();
        }
    }

    void FixedUpdate() {
        player1Forward = Input.GetAxisRaw("Vertical");
        player1Rotate = Input.GetAxis("Horizontal");

        updateForwardAndRotation(player1Forward, player1Rotate, playerRB, forwardSpeed, speedModifier, turnSpeed);

        if (Input.GetKey(KeyCode.Space)) {
            if (timer >= weaponShootSpeed) {
                timer = 0;
                shootWeapon(player, weaponType, weaponDamage);
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Projectile") {
            int damageTaken = other.gameObject.GetComponent<CannonScript>().damage;
            takeDamage(damageTaken);
        }
    }
}