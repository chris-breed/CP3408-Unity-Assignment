using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChunk : MonoBehaviour
{
    int[,] heights;
    int xsize, zsize;
    GridMesh mesh;
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
        for (int i = 0; i < xsize; i++)
        {
            for (int j = 0; j < zsize; j++)
            {
                heights[i, j] = map[xsize * x + i, zsize * z + j];
            }
        }
        enabled = true;
        mesh.Triangulate(heights);
    }

    void LateUpdate()
    {
        //mesh.Triangulate(heights);
        enabled = false;
    }
}

