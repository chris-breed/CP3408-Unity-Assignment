using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float speedMultiplier;
    Rigidbody playerRB;
    public GameObject turrentEnd;
    public GameObject turretPivotPoint;

    int floorMask;
    float camRayLength = 100f;

    float gunLength;

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody>();

        floorMask = LayerMask.GetMask("SeaSurface");

        gunLength = Vector3.Distance(turrentEnd.transform.position, turretPivotPoint.transform.position);
    }

    // Update is called once per frame
    void Update() {

        //w pressed move foward, a/d pressed rotate around y
        Movement();

        //rotateCannon();
    }

    private void rotateCannon() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 cannonDirection = floorHit.point - transform.position;
            cannonDirection.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(cannonDirection);


        }
    }

    private void Movement() {
        float turn = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.W)) {
            playerRB.AddForce(transform.forward * (speed * speedMultiplier) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) { //-
            rotationalTurn(turn);
        } else if (Input.GetKey(KeyCode.D)) { //+
            rotationalTurn(turn);
        }

        rotationalTurn(turn);
    }

    private void rotationalTurn(float turn) {
        transform.RotateAround(transform.position, transform.up, turn * Time.deltaTime * 90f);
    }
}
