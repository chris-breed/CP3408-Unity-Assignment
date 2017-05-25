using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public int defaultWeaponDamage = 2;
    //public int playerDamage;
    //public int shotDamage;

    public int playerFired;

	// Use this for initialization
	void Start () {
        //shotDamage = (defaultDamage * playerDamage);
	}

    public void setPlayerAndDamage(int player, int pDamage) {
        playerFired = player;
        //playerDamage = pDamage;

    }

}
