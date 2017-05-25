using UnityEngine;
using System.Collections;
using System;
using Boo.Lang;
using System.Collections.Generic;

public class mapGenerator : MonoBehaviour {

    private int width;
    private int length;
    public const int chunkCountX = 3, chunkCountZ = 3;

    public int start_pits = 20;
    //GridMesh mesh;
    public int average_depth = 3;

    [Range(0, 1)]
    public float water_surface_percent = 0.71f;

    public GridChunk chunkPrefab;
    //public Transform chunkPrefab;

    int[,] map;
    GridChunk[] chunks;

    int bomber_x = 0;
    int bomber_y = 0;

    System.Random random = new System.Random();

    //MeshGenAll[] meshMap;

    void Start() {
        //meshMap = new MeshGenAll[20];
        //for (int i = 0; i < meshMap.Length; i++)
        //for (int i = 0; i < 20; i++)
        //    {
        //    //MeshGenAll meshGen = gameObject.AddComponent<MeshGenAll>();
        //    //meshGen.GenerateMesh(map, 1);
        //    GameObject child = Instantiate(mesh_prefab, new Vector3(0, i-10, 0), Quaternion.identity);
        //    child.transform.parent = transform;
        //    GameObject child2 = Instantiate(wall_mesh_prefab, new Vector3(0, i - 10, 0), Quaternion.identity);
        //    child2.transform.parent = transform;
        //}
        width = Metrics.chunkSizeX * chunkCountX;
        length = Metrics.chunkSizeZ * chunkCountZ;
        //mesh = GetComponentInChildren<GridMesh>();
        CreateChunks();
        GenerateMap();
    }

    void CreateChunks()
    {
        chunks = new GridChunk[chunkCountX * chunkCountZ];

        for (int z = 0, i = 0;  z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                //GridChunk chunk = chunks[i++] = Instantiate(chunkPrefab, new Vector3(Metrics.chunkSizeX*Metrics.scale,0,Metrics.chunkSizeZ*z*Metrics.scale), Quaternion.identity);
                chunks[i] = Instantiate(chunkPrefab, new Vector3(Metrics.chunkSizeX * Metrics.scale * x, 0, Metrics.chunkSizeZ * z * Metrics.scale), Quaternion.identity);
                //GridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunks[i].transform.SetParent(transform);
                //chunks[i].setHeights(x,z, map);
                i++;
            }
        }
    }

    void UpdateChunks()
    {
        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                chunks[i].setHeights(x, z, map);
                i++;
            }
        }
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            //GenerateMap();
            BlastRandomHoles(1);
            updateWater();
        }
        //bomber_x += (int)Input.GetAxis("Horizontal");
        //bomber_y += (int)Input.GetAxis("Vertical");
        //if (Input.GetKeyDown("space"))
        //{
        //    BlastClean(bomber_x, bomber_y, 10, 1);
        //    updateWater();
        //}
    }
    void updateWater()
    {
        //while (WaterCount() > average_depth * width * length)
        while (WaterSurfaceCount() > water_surface_percent * width * length)
        {
            WaterDrain(1);
        }
        //UpdateMeshes();
        UpdateChunks();
        //mesh.Triangulate(map);
    }
    void GenerateMap()
    {
        map = new int[width, length];
        //RandomFillMap();
        BlastRandomHoles(start_pits);
        updateWater();
        //mesh.Triangulate(map);
        //MeshGenAll meshGen = GetComponent<MeshGenAll>();
        //meshGen.GenerateMesh(map, 1);

        //for (int i = 0; i < meshMap.Length; i++)
        //{
        //    meshMap[i].GenerateMesh(map, i ,1);
        //}
        //UpdateMeshes();
    }
    void UpdateMeshes()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshGenOne>() != null)
            {
                MeshGenOne meshGen = child.GetComponent<MeshGenOne>();
                meshGen.GenerateMesh(map, 1);
            }
        }
    }
    void BlastRandomHoles(int count)
    {
        while (count > 0)
        {
            int radius = random.Next(3, 30);
            BlastHole(random.Next(-radius, width+radius), random.Next(-radius, length+radius), radius, 0.5f);
            //BlastClean(random.Next(-radius, width + radius), random.Next(-radius, length + radius), radius, 0.5f);
            count--;
        }
    }
    void BlastHole(int x, int y, float radius, float depth)//subtracts from all area within the circle. explosion is a infinitely tall cone
    {
        int rad = (int)radius;
        for (int i = Mathf.Max(x-rad, 0); i< Mathf.Min(x+rad, width); i++)//iterates through with i starting at 0 or greater
        {
            for (int j = Mathf.Max(y -rad, 0); j < Mathf.Min(y + rad, length); j++)
            {   //distance from epicenter
                float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(i - x), 2f) + Mathf.Pow(Mathf.Abs(j - y), 2f));
                if (distance <= radius)
                {
                    int destruction = (int)((radius - distance) * depth);
                    map[i, j] -= destruction; 
                }

            }
        }
    }
    void BlastCleanish(int x, int y, float radius, float depth)//leaves a hole with a particular 3d shape. explosion has a height.
    {
        depth = depth * 3; //because i'm too lazy to change the code where it's called
        float blast_weight = .5f;   //how powerfully does the blast leave its particular shape?
        int rad = (int)radius;
        int z;
        if (x >= 0 && x < width && y >= 0 && y < length)
        {
            z = map[x, y];
        }
        else { z = 0; }
        for (int i = Mathf.Max(x - rad, 0); i < Mathf.Min(x + rad, width); i++)//iterates through with i starting at 0 or greater
        {
            for (int j = Mathf.Max(y - rad, 0); j < Mathf.Min(y + rad, length); j++)
            {   //distance from epicenter
                float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(i - x), 2f) + Mathf.Pow(Mathf.Abs(j - y), 2f));
                if (distance <= radius)
                {
                    int destruction = (int)((radius - distance) * depth);
                    if (map[i, j] > z - destruction)
                        { map[i, j] = (int)Mathf.Lerp(z - destruction, map[i, j], blast_weight); } //interpolates between old height and blast shape height
                }

            }
        }
    }
    public void BlowUp(int x, int y)
    {
        int h = 0;
        BlastInAir(x, y, h, 7.5f, 1);
        updateWater();
    }
    void BlastClean(int x, int y, float radius, float depth)//leaves a hole with a sphere shape
                                                             //simulation of a clean hole and then falling sand blasting on the ground
    {
        int rad = (int)radius;
        int z;
        if (x >= 0 && x < width && y >= 0 && y < length)
        {
            z = map[x, y];
        }
        else { z = 0; }
        for (int i = Mathf.Max(x - rad, 0); i < Mathf.Min(x + rad, width); i++)//iterates through with i starting at 0 or greater
        {
            for (int j = Mathf.Max(y - rad, 0); j < Mathf.Min(y + rad, length); j++)
            {   //distance from epicenter
                float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(i - x), 2f) + Mathf.Pow(Mathf.Abs(j - y), 2f));
                if (distance <= radius)
                {
                    int destruction = (int)(Math.Sqrt((radius*radius - distance*distance)) * depth);//sphere rather than cone
                    if (map[i, j] > z+destruction)
                    {
                        map[i, j] -= destruction*2;
                    }
                    else if (map[i, j] > z - destruction)
                    { map[i, j] = z - destruction; }
                }

            }
        }
    }
    void BlastInAir(int x, int y, int z, float radius, float depth)//leaves a hole with a sphere shape
                                                            //simulation of a clean hole and then falling sand
    {
        int rad = (int)radius;
        for (int i = Mathf.Max(x - rad, 0); i < Mathf.Min(x + rad, width); i++)//iterates through with i starting at 0 or greater
        {
            for (int j = Mathf.Max(y - rad, 0); j < Mathf.Min(y + rad, length); j++)
            {   //distance from epicenter
                float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(i - x), 2f) + Mathf.Pow(Mathf.Abs(j - y), 2f));
                if (distance <= radius)
                {
                    int destruction = (int)(Math.Sqrt((radius * radius - distance * distance)) * depth);//sphere rather than cone
                    if (map[i, j] > z + destruction)
                    {
                        map[i, j] -= destruction * 2;
                    }
                    else if (map[i, j] > z - destruction)
                    { map[i, j] = z - destruction; }
                }

            }
        }
    }

    int WaterCount()    //count volume of water
    {
        int volume = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (map[x, y] < 0)
                {
                    volume += -map[x,y];
                }
            }
        }
        return volume;
    }

    int WaterSurfaceCount()   //count surface area of water
    {
        int water = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (map[x, y] < 0)
                {
                    water += 1;
                }
            }
        }
        return water;
    }

    void WaterDrain(int amount)
    {
        //print("Water amount is: " + WaterCount());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                map[x, y] += amount;
            }
        }
    }

    void RandomFillMap() {

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                //if (x == 0 || x == width - 1 || y == 0 || y == length - 1) {
                   // map[x, y] = 1;
                //} else {
                    //map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                   map[x, y] = (random.Next(-10, 10));
                   
                
            }
        }
    }
    void FlattenMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                map[x, y] = 0;


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
                    float col = ((float)height + 10) / 15;
                    if (height < 0)
                    {
                        //Gizmos.color = Color.blue;
                        Gizmos.color = new Color(col, 1 + (float)height / 15, 1f, .1f);
                    }
                    else
                        //Gizmos.color = new Color(col, 1f, 0f, 1f);
                        Gizmos.color = new Color((float)height / 5, 1f, 0f, .1f);
                    //Vector3 pos = new Vector3(-width / 2 + x + .5f, height, -length / 2 + y + .5f);
                    //Gizmos.DrawCube(pos, Vector3.one);
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, height/2, -length / 2 + y + .5f);
                    Vector3 scale = new Vector3(1, height, 1);
                    Gizmos.DrawCube(pos, scale);
                }
            }
            int me_z = map[bomber_x, bomber_y] + 1;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(-width / 2 + bomber_x + .5f, me_z, - length / 2 + bomber_y + .5f), Vector3.one);
        }
    }
}