using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject mainMenuCanvas;
    public GameObject inGameHud;

    void Awake() {
        mainMenuCanvas.SetActive(true);
        inGameHud.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        //Menu Variables
        Button onePlayerGameButton = GameObject.Find("Play1PGame").GetComponent<Button>();
        onePlayerGameButton.onClick.AddListener(start1PGame);
        Button twoPlayerGameButton = GameObject.Find("Play2PGame").GetComponent<Button>();
        twoPlayerGameButton.onClick.AddListener(startP2Game);

        //InGameHud Variables
        int player1Health;
        int player2Health;
    }

    void Update() {

    }

    private void start1PGame() {
        Debug.Log("get a friend");
    }

    private void startP2Game() {
        //load testing scene
        mainMenuCanvas.SetActive(false);
        inGameHud.SetActive(true);
    }
}
