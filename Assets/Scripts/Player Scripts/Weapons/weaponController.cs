using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour {
    //base class for weapons, controls stuff like shooting speed, damage done, type of damage

    public GameObject firePoint;

    void Update() {

    }

    public void shootWeapon(int weaponType, GameObject prefab) {
            if (weaponType == 1) {
                shootCannon(prefab);
            
        }
    }

    void shootCannon(GameObject prefab) {

        GameObject projectile = Instantiate(prefab, firePoint.transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        Destroy(projectile, 5f);
    }
}

