using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //prefabs for weapons
    public Transform firePoint;
    public int bullet_speed = 3000;
    public float timeToBoom = .5f;
    public GameObject cannonShot;
    private int gridX, gridZ;
    private Rigidbody rigidBody;
    private int health = 100;
    static mapGenerator mapMother;
    //public CannonScript cannonScript;
    System.Random random = new System.Random();

    void Start()
    {
        mapMother = FindObjectOfType<mapGenerator>();
        rigidBody = GetComponent<Rigidbody>();
    }
    void Awake() {
        //cannonScript = GetComponent<CannonScript>();
    }

    public void updateForwardAndRotation(float speed, float turnSpeed) {
        //movement
        rigidBody.AddForce(transform.forward * (speed));
        //} else if (forward < 0) {
        //    rigidBody.velocity = rigidBody.velocity * 0.9f;
        //} else {
        //    rigidBody.velocity = rigidBody.velocity * 0.1f;

        //rotation
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f * turnSpeed);
    }

    public void shootWeapon(int player, int weaponType, int damage, int recoil_amount) {
        switch (weaponType) {
            case 1: //cannon
                GameObject projectile = Instantiate(cannonShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * bullet_speed);
                CannonScript bulletScript = projectile.GetComponent<CannonScript>();
                bulletScript.Invoke("Explode", timeToBoom);
                bulletScript.damage = damage;
                bulletScript.playerFired = player;
                break;
        }
        Vector3 recoil = -transform.forward * recoil_amount;
        rigidBody.AddForce(recoil);
    }

    void LateUpdate()
    {
        gridX = (int)Mathf.Round(transform.position.x / Metrics.scale);
        gridZ = (int)Mathf.Round(transform.position.z / Metrics.scale);
        if (gridX >= Metrics.xBlocks() || gridX < 0 || gridZ >= Metrics.zBlocks() || gridZ < 0)
        {
            return;
            //die();
        }
        else if (mapMother.getHeight(gridX, gridZ) * Metrics.getVScale() > transform.position.y)
        {
            die();
        }
    }

    bool bounce(float x, float z)
    {
        Vector3 normal = Vector3.zero;
        if (x >= Metrics.xBlocks())
            normal += Vector3.left;
        else if (x < 0)
            normal += Vector3.right;
        if (z >= Metrics.zBlocks())
            normal += Vector3.back;
        else if (z < 0)
            normal += Vector3.forward;
        if (normal == Vector3.zero)
        {
            return false;
        }
        rigidBody.velocity = Vector3.Reflect(rigidBody.velocity, normal);
        //Vector3 f = new Vector3(Random.value, Random.value, Random.value) * 20;
        //rigidBody.AddForce(f);
        return true;
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0)
            die();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            int damageTaken = other.gameObject.GetComponent<CannonScript>().damage;
            takeDamage(damageTaken);
        }
    }

    public void die() {
 
         //pause/stop game
         //decide winner (most kills, highest score, hit %, whatever)
         //show each players score

        transform.position = new Vector3(random.Next(Metrics.xBlocks()), 0, random.Next(Metrics.zBlocks())) * Metrics.scale;
        Debug.Log("Death x.x");
        health = 100;
        //throw new NotImplementedException();
    }
}