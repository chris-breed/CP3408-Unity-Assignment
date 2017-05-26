using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public GameObject mmc;
    public GameObject sc;

    // Use this for initialization
    void Start () {
        //Menu Variables
        Button onePlayerGameButton = GameObject.Find("Play1PGame").GetComponent<Button>();
        onePlayerGameButton.onClick.AddListener(start1PGame);
        Button twoPlayerGameButton = GameObject.Find("Play2PGame").GetComponent<Button>();
        twoPlayerGameButton.onClick.AddListener(startP2Game);
	}

    private void start1PGame() {
        Debug.Log("get a friend");
    }

    private void startP2Game() {
        //load testing scene
        SceneManager.LoadScene("Testing");
    }
}
