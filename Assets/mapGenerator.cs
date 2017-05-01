using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

    public int width;
    public int height;

    int[,] map;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int islandFillPercent;

    // Use this for initialization
    void Start() {
        GenerateMap();
    }

    private void GenerateMap() {
        map = new int[width, height];
        RandomFillMap();
    }

    private void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        int ones = 0;
        int zeros = 0;

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                    map[x, y] = 1;
                    ones++;
                } else {
                    map[x, y] = (pseudoRandom.Next(0, 100) < islandFillPercent) ? 1 : 0;
                    zeros++;      
                }
            }
        }
        Debug.Log(ones + " ones");
        Debug.Log(zeros + " zeros");
    }
    
    void Update() {

    }
}
