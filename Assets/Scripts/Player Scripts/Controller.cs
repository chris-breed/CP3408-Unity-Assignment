using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //prefabs for weapons
    public Transform firePoint;
    public int bullet_speed = 3000;
    public float bullet_time = .5f;
    public GameObject cannonShot;

    public CannonScript cannonScript;

    void Awake() {
        cannonScript = GetComponent<CannonScript>();
    }

    public void updateForwardAndRotation(float forward, float turn, Rigidbody RB, float speed, float speedMultiplier, float turnSpeed) {
        //movement
        if (forward > 0) {
            RB.AddForce(transform.forward * (speed * speedMultiplier));
        } else if (forward < 0) {
            RB.velocity = RB.velocity * 0.9f;
        } else {
            RB.velocity = RB.velocity * 0.1f;
        }

        //rotation
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }

    public void shootWeapon(int player, int weaponType, int damage) {
        switch (weaponType) {
            case 1:
                GameObject projectile = Instantiate(cannonShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * bullet_speed);
                CannonScript bulletScript = projectile.GetComponent<CannonScript>();
                bulletScript.Invoke("Explode", bullet_time);
                bulletScript.damage = damage;
                bulletScript.playerFired = player;

                break;
        }
    }


    public void takeDamage(int otherPlayersShotDamage) {

    }

    public void death() {
        //kill player
        /*
         pause/stop game
         decide winner (most kills, highest score, hit %, whatever)
         show each players score
         */
        throw new NotImplementedException();

    }
}