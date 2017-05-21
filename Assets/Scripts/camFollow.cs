using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public Transform player1;
    public Transform player2;

    Vector3 cameraHeight = new Vector3(0, 30f, 0);
    Vector3 pivotPoint; //should be center of both players

    Vector3 player1Pos;
    Vector3 player2Pos;

    Vector3 offset;

    Vector3 centreOfTwoPlayers;

    public float minZoom = 1;
    public float maxZoom = 10;

    public float distance;
    public float newFieldOfView;

    // Use this for initialization
    void Start() {
        newFieldOfView = 30f;

    }

    // Update is called once per frame
    void Update() {
        player1Pos = player1.position;
        player2Pos = player2.position;
        pivotPoint = Vector3.Lerp(player1Pos, player2Pos, 0.5f);
        distance = Vector3.Distance(player1Pos, player2Pos);

        Vector3 newCameraPosition = pivotPoint;


        transform.position = newCameraPosition + cameraHeight;
        transform.LookAt(pivotPoint);

        adjustFieldOfView(distance);

    }

    private void adjustFieldOfView(float distance) {

        if (distance < 10) {
            newFieldOfView = 30f;
        } else {

        }


        if (distance > 40) {
            newFieldOfView = 80f;
        }

        Camera.main.fieldOfView = newFieldOfView;

    }
}
