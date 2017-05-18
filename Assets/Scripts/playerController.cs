using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float speedMultiplier;
    public float turnSpeed;

    public Rigidbody playerRB;

    public GameObject bulletPrefab;
    public GameObject endOfBarrel;

    public int playerNumber;

    void Start() {
        playerRB = GetComponent<Rigidbody>();


        if (tag.Equals("Player")) {
            playerNumber = 0;
        } else if (tag.Equals("Player2")) {
            playerNumber = 1;
        }
    }

    void Update() {
        //Player 1 controlls
        //a rotates left, d rotates right, w moves forward
        //space to shoot
        movePlayer();
        rotatePlayer();
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootProjectile();
        }

        //Player 1 controlls
        //left arrow rotates left, right arrow rotates right, up arrow moves forward
        //numberpad 5 to shoot
        movePlayer2();
        rotatePlayer2();
        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            shootProjectile2();
        }
        
    }

    private void shootProjectile() {
        GameObject projectile = Instantiate(bulletPrefab, endOfBarrel.transform.position, Quaternion.identity) as GameObject;

        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        Destroy(projectile, 20f);

    }

    private void shootProjectile2() {
        GameObject projectile = Instantiate(bulletPrefab, endOfBarrel.transform.position, Quaternion.identity) as GameObject;

        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);

        Destroy(projectile, 20f);
    }

    private void rotatePlayer2() {
        float forwardMove = Input.GetAxisRaw("Vertical2");
        if (forwardMove > 0) {
            playerRB.AddForce(transform.forward);
        }
    }

    private void movePlayer2() {
        float turn = Input.GetAxisRaw("Horizontal2");
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);
    }

    private void rotatePlayer() {
        float turn = Input.GetAxisRaw("Horizontal");
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f * turnSpeed);

    }

    private void movePlayer() {
        float forwardMove = Input.GetAxisRaw("Vertical");
        if (forwardMove > 0) {
            playerRB.AddForce(transform.forward);
        }
    }
}
