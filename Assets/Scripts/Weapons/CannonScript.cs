using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public int damage = 2;
    //public int playerDamage;
    //public int shotDamage;
    public int playerFired;
    int gridX;
    int gridZ;
    static mapGenerator mapMother;
    Controller controller;
	void Start () {
        mapMother = FindObjectOfType<mapGenerator>();
        controller = FindObjectOfType<Controller>();
    }

    public void setPlayerAndDamage(int player, int pDamage) {
        playerFired = player;
        //playerDamage = pDamage;

    }

    void Explode() {
        mapMother.BlowUp(gridX,gridZ);
        controller.takeDamage(damage);
        Destroy(gameObject);
    }

    void Update()
    {
        gridX = (int)Mathf.Round(transform.position.x / Metrics.scale);
        gridZ = (int)Mathf.Round(transform.position.z / Metrics.scale);
        if (gridX<Metrics.xBlocks() && gridX >= 0 && gridZ < Metrics.zBlocks() && gridZ >= 0 && mapMother.getHeight(gridX, gridZ) * Metrics.getVScale() > transform.position.y)
        {
            Explode();
        }

    }
    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject.tag == "Terrain") {
    //        Explode();

    //    }
    //}
}
