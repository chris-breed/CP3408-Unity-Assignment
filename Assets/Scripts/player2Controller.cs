using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Controller : MonoBehaviour {

    public float speed;
    public float speedMultiplier;
    public float turnSpeed;

    public Rigidbody playerRB;

    public GameObject bulletPrefab;
    public GameObject endOfBarrel;

    //player 2
    float player2Forward;
    float player2Rotate;

    void Start() {

    }

    void Update() {
  
        //player 2 controls
        //left rotates left, right rotates right, up moves forward
        //numpad5 to shoot

        //update player 2 info
        player2Forward = Input.GetAxisRaw("P2_Vertical");
        player2Rotate = Input.GetAxis("P2_Horizontal");

        updatePlayer(player2Forward, player2Rotate);

        if (Input.GetKey(KeyCode.Keypad5)) {
            shootProjectile();
        }

    }
    
    private void updatePlayer(float forward, float turn) {
        //movement
        if (forward < 0) {
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
