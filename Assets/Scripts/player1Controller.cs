using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1Controller : MonoBehaviour {

    public float speed;
    public float speedMultiplier;
    public float turnSpeed;

    public Rigidbody playerRB;

    public GameObject bulletPrefab;
    public GameObject endOfBarrel;

    //player 1
    float player1Forward;
    float player1Rotate;

    void Start() {

    }

    void Update() {
        //Player 1 controls
        //a rotates left, d rotates right, w moves forward
        //space to shoot

        //update player 1 info
        player1Forward = Input.GetAxisRaw("Vertical");
        player1Rotate = Input.GetAxis("Horizontal");



        updatePlayer(player1Forward, player1Rotate);

        if (Input.GetKey(KeyCode.Space)) {
            shootProjectile();
        }

    }

    private void updatePlayer(float forward, float turn) {
        //movement
        if (forward > 0) {
            playerRB.AddForce(transform.forward);
        }


        //rotation
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }

    private void shootProjectile() {
        GameObject projectile = Instantiate(bulletPrefab, endOfBarrel.transform.position, Quaternion.identity) as GameObject;

        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        Destroy(projectile, 5f);

    }
}
