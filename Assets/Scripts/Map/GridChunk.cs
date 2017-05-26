using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChunk : MonoBehaviour
{
    int[,] heights;
    int xsize, zsize;
    GridMesh mesh;
    public bool needsUpdate;
    void Awake()
    {
        xsize = Metrics.chunkSizeX;
        zsize = Metrics.chunkSizeZ;
        heights = new int[xsize, zsize];
        mesh = GetComponentInChildren<GridMesh>();
        enabled = false;
    }
    public void setHeights(int x, int z, int[,] map)
    {
        //for (int i = 0; i < xsize; i++)
        //{
        //    for (int j = 0; j < zsize; j++)
        //    {
        //        heights[i, j] = map[xsize * x + i, zsize * z + j];
        //    }
        //}
        enabled = true;
        mesh.Triangulate(map, x,z);
        for (int i = x * Metrics.chunkSizeX; i < (x+1) * Metrics.chunkSizeX; i++)
        {
            for (int j = z * Metrics.chunkSizeZ; j < (z+1) * Metrics.chunkSizeZ; j++)
            {
                //Triangulate(i, j, heightMap[i,j]);
                createCollider(i, j, map);
            }
        }
        needsUpdate = false;
    }

    void LateUpdate()
    {
        //mesh.Triangulate(heights);
        enabled = false;
    }
    public void createCollider(int i, int j, int[,] cells)
    {
        if (cells[i, j] >= 0 && isBorder(i, j, cells))
        {
            GameObject baby = new GameObject("ColliderBaby");
            baby.transform.parent = transform;
            //BoxCollider collider = baby.gameObject.AddComponent<BoxCollider>();
            SphereCollider collider = baby.gameObject.AddComponent<SphereCollider>();
            collider.center = new Vector3(i, 0, j) * Metrics.scale;
            collider.radius = Metrics.scale/2;
        }
    }
    public bool isBorder(int i, int j, int[,] cells)
    {
        //(i == 0 || i == cells.GetLength(0) - 1 || j == 0 || j == cells.GetLength(1) - 1)
        if ((i != cells.GetLength(0) - 1 && cells[i + 1, j] < 0) || (i != 0 && cells[i - 1, j] < 0) || (j != cells.GetLength(0) - 1 && cells[i, j + 1] < 0) || (j != 0 && cells[i, j - 1] < 0))
        {
            return true;
        }
        return false;

    }
}

