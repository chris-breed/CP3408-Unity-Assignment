using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Script : Controller {

    //misc variables
    public Rigidbody playerRB;
    float timer;

    int player; //2

    //movement variables
    public float forwardSpeed;
    public float speedModifier;
    public float turnSpeed;

    //health variables
    public int health;

    //weapon variable
    public int weaponType; //1 = cannon
    public int weaponShootSpeed;
    public int playerDefaultDamage;

    float player1Forward;
    float player1Rotate;

    private void Awake() {
        player = 2;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (health <= 0) {
            death();
        }

        player1Forward = Input.GetAxisRaw("P2_Vertical");
        player1Rotate = Input.GetAxis("P2_Horizontal");

        updateForwardAndRotation(player1Forward, player1Rotate, playerRB, forwardSpeed, speedModifier, turnSpeed);

        if (Input.GetKey(KeyCode.Keypad5)) {
            if (timer >= weaponShootSpeed) {
                timer = 0;
                shootWeapon(player, weaponType, playerDefaultDamage);
            }
        }
    }
}