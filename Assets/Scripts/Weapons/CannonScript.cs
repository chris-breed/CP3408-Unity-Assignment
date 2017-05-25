using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public int damage = 2;
    //public int playerDamage;
    //public int shotDamage;
    public int playerFired;

    // Use this for initialization
    void Start() {
        //shotDamage = (defaultDamage * playerDamage);

        Invoke("Explode", 2);
    }

    public void setPlayerAndDamage(int player, int pDamage) {
        playerFired = player;
        //playerDamage = pDamage;

    }

    void Explode() {
        int gridX = (int)Mathf.Round(transform.position.x / Metrics.scale);
        int gridY = (int)Mathf.Round(transform.position.x / Metrics.scale);
        mapGenerator mapMother = FindObjectOfType<mapGenerator>();
        mapMother.BlowUp(gridX, gridY);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Terrain") {
            Explode();

        }
    }
}
