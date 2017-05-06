using UnityEngine;
using System.Collections;
using System;
using Boo.Lang;
using System.Collections.Generic;

public class mapGenerator : MonoBehaviour {

    public int width;
    public int length;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;
    //int[] heightMap;

    Dictionary<int[,], int> elevation;

    void Start() {
        GenerateMap();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap();
        }
    }

    void GenerateMap() {
        map = new int[width, length];
        RandomFillMap();

        for (int i = 0; i < 5; i++) {
            //SmoothMap();
        }

        //RaiseLand();

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        //meshGen.GenerateMesh(map, 1);
    }

    private void RaiseLand() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                if (map[x, y] == 1) {

                    //elevation.Add(x, y, 1);

                }
                if (map[x, y] == 0) {
                    //elevation.Add(x, y, 0);
                }

            }
        }



    }

    void RandomFillMap() {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                //if (x == 0 || x == width - 1 || y == 0 || y == length - 1) {
                   // map[x, y] = 1;
                //} else {
                    //map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                   map[x, y] = (pseudoRandom.Next(-10, 10));
                   
                
            }
        }
    }

    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < length) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += map[neighbourX, neighbourY];
                    }
                } else {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }


    void OnDrawGizmos() {

        if (map != null) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < length; y++) {
                    //Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    int height = map[x, y];
                    float col = ((float)height + 10) / 20;
                    Gizmos.color = new Color(col, col, 1f, 1f);
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, height, -length / 2 + y + .5f);
                    //Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -length / 2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }

    }

}