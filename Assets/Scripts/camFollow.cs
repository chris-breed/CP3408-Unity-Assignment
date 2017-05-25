using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public float viewSize;

    public Transform player1;
    public Transform player2;

    float cameraHeight = 30f;
    Vector3 pivotPoint; //should be center of both players

    Vector3 player1Pos;
    Vector3 player2Pos;

    Vector3 offset;

    Vector3 centreOfTwoPlayers;

    public float distance;
    public float newCameraHeight;
    public float maxCameraHeight;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void LateUpdate() {
        player1Pos = player1.position;
        player2Pos = player2.position;
        pivotPoint = Vector3.Lerp(player1Pos, player2Pos, 0.5f);
        distance = Vector3.Distance(player1Pos, player2Pos);

        Vector3 newCameraPosition = pivotPoint;
        newCameraPosition.y = cameraHeight;
        transform.position = newCameraPosition;

        adjustCameraHeight(distance);

    }

    private void adjustCameraHeight(float distance) {

        if (distance <= 10) {
            newCameraHeight = 30f;
        }

        if (distance >= 10) {
            newCameraHeight = distance * viewSize;
            if (newCameraHeight >= maxCameraHeight) {
                newCameraHeight = maxCameraHeight;
            }
        }

        

        Camera.main.transform.position = new Vector3(transform.position.x, newCameraHeight, transform.position.z);


    }
}
