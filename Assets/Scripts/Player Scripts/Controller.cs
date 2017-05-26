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
    private int gridX, gridZ;
    static mapGenerator mapMother;
    public CannonScript cannonScript;
    System.Random random = new System.Random();

    void Start()
    {
        mapMother = FindObjectOfType<mapGenerator>();
    }
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

    void Update()
    {
        gridX = (int)Mathf.Round(transform.position.x / Metrics.scale);
        gridZ = (int)Mathf.Round(transform.position.z / Metrics.scale);
        if (gridX >= Metrics.xBlocks() || gridX < 0 || gridZ >= Metrics.zBlocks() || gridZ < 0)
        {
            die();
        }
        else if (mapMother.getHeight(gridX, gridZ) * Metrics.getVScale() > transform.position.y)
        {
            die();
        }
    }


    public void takeDamage(int otherPlayersShotDamage) {

    }

    public void die() {
 
         //pause/stop game
         //decide winner (most kills, highest score, hit %, whatever)
         //show each players score

        transform.position = new Vector3(random.Next(Metrics.xBlocks()), 0, random.Next(Metrics.zBlocks())) * Metrics.scale;
        Debug.Log("Death x.x");
        //health = 100;
        //throw new NotImplementedException();
    }
}