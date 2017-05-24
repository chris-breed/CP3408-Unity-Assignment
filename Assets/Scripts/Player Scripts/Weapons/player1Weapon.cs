using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1Weapon : weaponController {

    public GameObject prefab;

    public int weapon = 1; //1 cannon

    float weaponTimer;

    //cannonvariables
    public int weaponShootSpeed = 1; //in seconds
    //public int weaponDamage = 1;
    //public int energyDrain = 2;

    void Update() {
        weaponTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) {
            if (weaponTimer >= weaponShootSpeed) {
                weaponTimer = 0;
                shootWeapon(weapon, prefab);
            }
        }
    }
}
