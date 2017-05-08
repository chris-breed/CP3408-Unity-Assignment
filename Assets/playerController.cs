using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    float fowardSpeed = 5f;
    float torqueSpeed = 3f;
    Rigidbody playerRB;
    public Rigidbody cannonRB;

    Vector3 cannonPivot;

    int floorMask;
    float camRayLength = 100f;

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody>();
                
        cannonPivot.Set(transform.position.x, gameObject.transform.GetChild(1).transform.position.y, transform.position.z); 

        floorMask = LayerMask.GetMask("SeaSurface");

    }

    // Update is called once per frame
    void Update() {
        float turn = Input.GetAxisRaw("Horizontal");
        //w pressed move foward, a/d pressed rotate around y

        Movement(turn);
        //rotateCannon();
    }

    private void rotateCannon() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 cannonDirection = floorHit.point - transform.position;
            cannonDirection.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(cannonDirection);

            cannonRB.MoveRotation(newRotation);
        }
    }

    private void Movement(float turn) {
        if (Input.GetKey(KeyCode.W)) {
            playerRB.AddForce(Vector3.forward * fowardSpeed * Time.deltaTime);
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
