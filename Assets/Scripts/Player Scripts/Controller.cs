using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //prefabs for weapons
    public Transform firePoint;

    public GameObject cannonShot;

    public void updateForwardAndRotation(float forward, float turn, Rigidbody RB, float speed, float speedMultiplier, float turnSpeed) {
        //movement
        if (forward > 0) {
            RB.AddForce(transform.forward * (speed * speedMultiplier));
        }

        //rotation
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }

    public void shootWeapon(int weaponType, int damage) {
        switch (weaponType) {
            case 1:
                GameObject projectile = Instantiate(cannonShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
                Destroy(projectile, 5f);
                break;
        }
    }

    public void death() {
        //kill player
        /*
         pause game
         decide winner
         show each players score
         */
        throw new NotImplementedException();

    }
}