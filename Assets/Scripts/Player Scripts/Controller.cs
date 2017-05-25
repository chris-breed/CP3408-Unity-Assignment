using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //prefabs for weapons
    public Transform firePoint;

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
            RB.velocity = RB.velocity * 0.7f;
        }

        //rotation
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }

    public void shootWeapon(int player, int weaponType, int damage) {
        switch (weaponType) {
            case 1:
                GameObject projectile = Instantiate(cannonShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
                CannonScript bulletScript = projectile.GetComponent<CannonScript>();

                bulletScript.defaultWeaponDamage = damage;
                bulletScript.playerFired = player;
                Destroy(projectile, 5f);
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