using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Controller
{

    //misc variables
    Rigidbody myRB;
    Transform target;
    float timer;

    public int player = 3;

    //movement variables
    public float turnSpeed = 5;
    public float speed = 20;
    //health variables

    //weapon variable
    public int weaponType = 1; //1 = gunn
    public float weaponShootSpeed = 0.1f;
    public int weaponDamage = 1;

    bool shooting = true;

    float playerForward;
    float turnInput;
    System.Random random;
    void Awake()
    {
        die();
        random = new System.Random();
        playerScript[] players = FindObjectsOfType<playerScript>();
        target = players[random.Next(players.Length)].GetComponent<Transform>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        playerForward = 1;
        turnInput = 1;
        turnInput = -1;
        turnInput = 0;
        timer -= Time.deltaTime;

        if (shooting)
        {
            if (timer < 0)
            {
                timer += weaponShootSpeed;
                shootWeapon(player);
            }
        }
        if (activeWeapon == 1)
        {
            if (distanceToPlayer > 40)
                switchWeapon();
        }
        else if (distanceToPlayer < 15)
        {
            switchWeapon();
        }
    }

    void FixedUpdate()
    {
        updateForwardAndRotation(playerForward * speed, turnInput * turnSpeed);
    }
}