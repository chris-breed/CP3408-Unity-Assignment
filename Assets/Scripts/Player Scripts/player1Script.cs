using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1Script : Controller {

    //misc variables
    public Rigidbody playerRB;
    float timer;

    //movement variables
    float forwardSpeed;
    float speedModifier;
    float turnSpeed;

    //health variables
    int health;

    //weapon variable
    int weaponType; //1 = cannon
    int weaponShootSpeed;
    int weaponDamage;
  

    float player1Forward;
    float player1Rotate;


    void Awake () {
        initMovementVariables();
        initWeaponVariables();
        initPlayerStatsVariables();
	}

    private void initWeaponVariables() {
        weaponType = 1;
        weaponShootSpeed = 2;
        weaponDamage = 5;
    }

    private void initMovementVariables() {
        forwardSpeed = 1;
        speedModifier = 5;
        turnSpeed = 5;
    }

    private void initPlayerStatsVariables() {
        health = 100;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

		if(health <= 0) {
            death();
        }

        player1Forward = Input.GetAxisRaw("Vertical");
        player1Rotate = Input.GetAxis("Horizontal");

        updateForwardAndRotation(player1Forward, player1Rotate, playerRB, forwardSpeed, speedModifier, turnSpeed);

        if (Input.GetKey(KeyCode.Space)) {
            if (timer >= weaponShootSpeed) {
                timer = 0;
                shootWeapon(weaponType, weaponDamage);
            }
        }
    }

    private new void death() {
        //kill player
        throw new NotImplementedException();
    }
}