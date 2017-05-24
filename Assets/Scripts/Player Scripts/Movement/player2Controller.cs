﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Controller : playerController {

    public Rigidbody playerRB;

    public float bSpeed;
    public float baseSpeedMultiplier;
    public float baseTurnSpeed;

    //player 1
    float player1Forward;
    float player1Rotate;

    void Start() {

        stats(playerRB, bSpeed, baseSpeedMultiplier, baseTurnSpeed);
    }

    void Update() {
        //update player 1 info
        player1Forward = Input.GetAxisRaw("P2_Vertical");
        player1Rotate = Input.GetAxis("P2_Horizontal");

        updateForwardAndRotation(player1Forward, player1Rotate);
    }
}
