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
    public GameObject winScreen;
    public GameObject player1;
    public GameObject player2;
    public GameObject NPC;
    public camFollow cam;

    void Awake() {
        mainMenuCanvas.SetActive(true);
        inGameHud.SetActive(false);
        winScreen.SetActive(false);
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

  

    public void endGame() {
        winScreen.SetActive(true);
    }

    private void start1PGame() {
        //Debug.Log("get a friend");
        mainMenuCanvas.SetActive(false);
        inGameHud.SetActive(true);
        cam.setPlayers(Instantiate(player1),(Instantiate(NPC)));
    }

    private void startP2Game() {
        //load testing scene
        cam.setPlayers(Instantiate(player1), (Instantiate(player2)));
        mainMenuCanvas.SetActive(false);
        inGameHud.SetActive(true);
    }
}
