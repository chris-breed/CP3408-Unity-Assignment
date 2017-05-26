using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Script : Controller {

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
    public int weaponDamage = 1;

    float player2Forward;
    float player2Rotate;

    private void Awake() {
        player = 2;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (health <= 0) {
            die();
        }
    }

    void FixedUpdate() {
        player2Forward = Input.GetAxisRaw("P2_Vertical");
        player2Rotate = Input.GetAxis("P2_Horizontal");

        updateForwardAndRotation(player2Forward, player2Rotate, playerRB, forwardSpeed, speedModifier, turnSpeed);

        if (Input.GetKey(KeyCode.Keypad5)) {
            if (timer >= weaponShootSpeed) {
                timer = 0;
                shootWeapon(player, weaponType, weaponDamage);
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Projectile") {
            int damageTaken = other.gameObject.GetComponent<CannonScript>().damage;
            takeDamage(damageTaken);
        }
    }
}