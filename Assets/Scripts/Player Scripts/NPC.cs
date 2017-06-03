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
    public int aimError = 10; //in degrees

    bool shooting = true;

    float playerForward;
    float turnInput;
    System.Random random;
    void Awake()
    {
        random = new System.Random();
        playerScript[] players = FindObjectsOfType<playerScript>();
        target = players[random.Next(players.Length)].GetComponent<Transform>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        print("distance is " + distanceToPlayer);
        timer -= Time.deltaTime;

        if (shooting)
        {
            turnMe();
            if (timer < 0)
            {
                if (timer < 0)
                {
                    if (activeWeapon == 1)
                        timer += gun_fire_rate;
                    else
                        timer += cannon_fire_rate;
                    shootWeapon(player);
                }
            }
            if (activeWeapon == 1)
            {
                if (distanceToPlayer >8f)
                {
                    shooting = false;
                }
                else if (distanceToPlayer < 1f)
                {
                    switchWeapon();
                }
            }
        }
        else
        {
            turnInput = 0;
            if (distanceToPlayer < 3f)
            {
                shooting = true;
            }
        }
        if (activeWeapon == 0 && distanceToPlayer > 1f)
        {
                switchWeapon();
        }
        if (distanceToPlayer > 2f)
        {
            turnMe();
            playerForward = 1;
        }
        else if (distanceToPlayer < .4f)
        {
            playerForward = 0;
        }

    }

    void turnMe()
    {
        Vector3 aim = target.position - transform.position; //vector pointing to target
        //float turn = Vector3.Angle(aim, transform.forward);//returns POSITIVE angle between ugh
        float turn_direction = Vector3.Cross(transform.forward, aim).y;
        if (turn_direction < -aimError)
        {    //workaround using cross product
            //turn = -turn;
            turnInput = -1;
        }
        else if (turn_direction > aimError)
        {    //workaround using cross product
            //turn = -turn;
            turnInput = 1;
        }
        else
        {
            turnInput = 0;
        }
        //turnInput = turn/10;
        if (turnInput < -0.5f) { turnInput = -0.5f; }
        if (turnInput > 0.5f) { turnInput = 0.5f; }
    }

    void FixedUpdate()
    {
        updateForwardAndRotation(playerForward * speed, turnInput * turnSpeed);
        //updateForwardAndRotation(playerForward * speed, 0);
    }
}