using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //prefabs for weapons
    public Transform firePoint;
    public int bullet_speed = 3000;
    public int cannon_speed = 1000;
    public int cannon_up = 1000;
    public float timeToBoom_bullet = .5f;
    public float timeToBoom_cannon = 2f;
    public GameObject cannonShot;
    public GameObject bulletShot;
    public float bullet_recoil = 50;
    public float cannon_recoil = 800;
    private int gridX, gridZ;
    private Rigidbody rigidBody;
    static mapGenerator mapMother;
    public int activeWeapon = 1;
    //public CannonScript cannonScript;
    System.Random random = new System.Random();

    public float cannon_fire_rate = 1.5f;
    public float gun_fire_rate = .2f;
    private int health = 100;
    private int lives = 1;
    MenuScript menuScript;


    public GameObject explosionprefab;


    private int health = 100;
    void Start()
    {
        mapMother = FindObjectOfType<mapGenerator>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
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
    public void switchWeapon()
    {
        activeWeapon = (activeWeapon + 1) % 2;
    }
    public void shootWeapon(int player) {
        GameObject projectile;
        CannonScript script;
        Vector3 recoil;
        switch (activeWeapon) {
            case 1: //fast small bullets
            {
                projectile = Instantiate(bulletShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * bullet_speed);
                script  = projectile.GetComponent<CannonScript>();
                script.Invoke("Explode", timeToBoom_bullet);
                script.playerFired = player;
                recoil = -transform.forward * bullet_recoil;
                break;
            }
            default: //big cannonball
                projectile = Instantiate(cannonShot, firePoint.transform.position, Quaternion.identity) as GameObject;
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * cannon_speed+transform.up*cannon_up);
                script = projectile.GetComponent<CannonScript>();
                script.Invoke("Explode", timeToBoom_cannon);
                script.playerFired = player;
                recoil = -transform.forward * cannon_recoil;
                break;
        }
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
            Debug.Log(damageTaken);
        }
    }

    public void die() {
 
         //pause/stop game
         //decide winner (most kills, highest score, hit %, whatever)
         //show each players score

        transform.position = new Vector3(random.Next(Metrics.xBlocks()), 0, random.Next(Metrics.zBlocks())) * Metrics.scale;
        //Debug.Log("Death x.x");
        health = 100;
        //throw new NotImplementedException();
    }
}