using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour {
    //public int xSize, ySize;
    [Range(0.0f, 1.0f)]
    public float platformSize = 1f;
    public const float verticalScale = 1f;
    //private int[,] map;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    private Mesh mesh;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }
    public void Triangulate(int[,] cells)
    {
        mesh.Clear();
        vertices.Clear();
        colors.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                        //Triangulate(i, j, heightMap[i,j]);
                        Triangulate(i, j, cells);
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();
    }
    Vector3 GetCenter(int x, int z, int[,] map)
    {
        Vector3 center = new Vector3(x, map[x, z]*verticalScale, z) * Metrics.scale;
        return center;
    }
    Color makeColor(float height)
    {
        Color color;
        if (height < 0)
        {
            //Gizmos.color = Color.blue;
            float colVal = (height + 5) / 15;
            color = new Color(colVal, 1 + height / 15, 1f, .1f);
        }
        else
            //Gizmos.color = new Color(col, 1f, 0f, 1f);
            color = new Color(.7f-height / 3 + height * height / 30, .8f+height/10-height*height/70, .3f-height/8+ height * height / 100, .1f);
        return color;
    }
    void Triangulate(int x, int z, int[,] heights)
    {
        Vector3 center = GetCenter(x, z, heights);
        Vector3 up_right = new Vector3(1, 0, 1) * Metrics.scale / 2*platformSize;
        Vector3 up_left = new Vector3(-1, 0, 1) * Metrics.scale / 2 * platformSize;
        Color color = makeColor(heights[x, z]);
        AddQuad(//floor
            center - up_right,
            center - up_left,
            center + up_left,
            center + up_right, color);
        if (x > 0)//walls
        {
            Vector3 other_center = GetCenter(x - 1, z, heights);
            AddQuad(
            center - up_right,
            center + up_left,
            other_center - up_left,
            other_center + up_right, makeColor(((float)heights[x,z]+ (float)heights[x-1,z])/2));
        }
        if (z > 0)
        {
            Vector3 other_center = GetCenter(x, z - 1, heights);
            AddQuad(
            center - up_left,
            center - up_right,
            other_center + up_right,
            other_center + up_left, makeColor(((float)heights[x, z] + (float)heights[x, z-1]) / 2));
        }
        if (z > 0 & x > 0)//diagonals
        {
            AddQuad(
            center - up_right,
            GetCenter(x - 1, z, heights) - up_left,
            GetCenter(x, z - 1, heights) + up_left,
            GetCenter(x - 1, z - 1, heights) + up_right, makeColor(((float)heights[x, z] + (float)heights[x - 1, z - 1] + (float)heights[x - 1, z] + (float)heights[x, z - 1]) / 5));
        }

        //        {
        //            AddTriangle(
        //              GetCenter(x - 1, z, heights) - up_left,
        //              center - up_right,
        //              GetCenter(x - 1, z - 1, heights) + up_right, makeColor((heights[x, z] + heights[x - 1, z - 1]) / 2 -20));
        //            AddTriangle(
        //            center - up_right,
        //            GetCenter(x, z - 1, heights) + up_left,
        //            GetCenter(x - 1, z - 1, heights) + up_right, makeColor((heights[x, z] + heights[x - 1, z - 1]) - 20));
        //        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    //void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    //{
    //    int vertexIndex = vertices.Count;
    //    vertices.Add(v1);
    //    vertices.Add(v2);
    //    vertices.Add(v3);
    //    vertices.Add(v4);
    //    triangles.Add(vertexIndex);
    //    triangles.Add(vertexIndex + 2);
    //    triangles.Add(vertexIndex + 1);
    //    triangles.Add(vertexIndex + 1);
    //    triangles.Add(vertexIndex + 2);
    //    triangles.Add(vertexIndex + 3);
    //}
    void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
    }
}
