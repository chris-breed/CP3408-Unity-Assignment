﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public int damage = 2;
    //public int playerDamage;
    //public int shotDamage;
    public int playerFired;

	// Use this for initialization
	void Start () {
        Explode();
        //shotDamage = (defaultDamage * playerDamage);
    }

    public void setPlayerAndDamage(int player, int pDamage) {
        playerFired = player;
        //playerDamage = pDamage;

    }

    void Explode()
    {
        int gridX = (int)Mathf.Round(transform.position.x / Metrics.scale);
        int gridY = (int)Mathf.Round(transform.position.z / Metrics.scale);
        mapGenerator mapMother = GameObject.FindObjectOfType<mapGenerator>();
        mapMother.BlowUp(gridX,gridY);
    }

}
