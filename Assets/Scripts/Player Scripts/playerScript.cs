using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : Controller
{
    public String xInput = "Vertical";
    public String zInput = "Horizontal";
    public KeyCode fireButton = KeyCode.Space;
    public KeyCode changeWeaponButton = KeyCode.LeftShift;
    //misc variables
    Rigidbody playerRB;
    float timer;

    public int player = 1;

    //movement variables
    public float turnSpeed = 5;
    public float speed = 20;
    //health variables

    //weapon variable
    public int weaponType; //1 = gunn
    public float weaponShootSpeed = 0.1f;
    public int weaponDamage = 1;

    float playerForward;
    float turnInput;

    void Awake()
    {
        die();
    }

    void Update()
    {

        playerForward = Input.GetAxisRaw(xInput);
        turnInput = Input.GetAxis(zInput);
        timer -= Time.deltaTime;

        if (Input.GetKey(fireButton))
        {
            if (timer < 0)
            {
                timer += weaponShootSpeed;
                shootWeapon(player);
            }
        }
        if (Input.GetKey(changeWeaponButton))
        {
            switchWeapon();
        }
    }

    void FixedUpdate()
    {
        updateForwardAndRotation(playerForward*speed, turnInput * turnSpeed);
    }
}