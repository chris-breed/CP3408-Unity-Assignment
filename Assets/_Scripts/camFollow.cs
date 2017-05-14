using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour {

    public Transform player;

    [Range(0f, 10f)]
    public float cameraSpeed = 4f;
    Vector3 offset;

    // Use this for initialization
    void Start() {
        offset = transform.position - player.position;

    }

    // Update is called once per frame
    void Update() {
        Vector3 oldCameraPosition = transform.position;
        Vector3 newCameraPosition = player.position + offset;

        transform.position = Vector3.Lerp(oldCameraPosition, newCameraPosition, 1f);
    }
}
