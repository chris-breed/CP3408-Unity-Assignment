using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public float viewSize = 3;
    public float minHeight = 5;
    [Range(0, 1)]
    public float camSpeed = .1f;

    public Transform player1;
    public Transform player2;

    Vector3 offset;
    public float distance;

    Vector3 centreOfTwoPlayers;
    //public float maxCameraHeight;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void LateUpdate() {
        float center_x = Mathf.Lerp(player1.position.x, player2.position.x, 0.5f);
        float center_z = Mathf.Lerp(player1.position.z, player2.position.z, 0.5f);
        distance = Vector3.Distance(player1.position, player2.position);

        //newCameraPosition.y = cameraHeight;
        //transform.position = newCameraPosition;

        float goalHeight = distance * viewSize;
        if (goalHeight < minHeight)
        {
            goalHeight = minHeight;
        }

        Vector3 goalPosition = new Vector3(center_x, goalHeight, center_z);
        Vector3 newCameraPosition = Vector3.Lerp(transform.position, goalPosition, camSpeed);

        //Camera.main.transform.position = newCameraPosition;
        transform.position = newCameraPosition;
        transform.LookAt(new Vector3(center_x, 0, center_z), Vector3.forward);
        //Camera.main.transform.position = new Vector3(transform.position.x, camHeight, transform.position.z);


    }
}
