using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LandGen : MonoBehaviour
{
    /// <summary>
    /// Height map texture
    /// </summary>
    public Texture2D Texture
    {
        get
        {
            Texture2D texture = new Texture2D(256, 256, TextureFormat.RGBA32, 0, true);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    float xCoord = xShift + (float)x / (float)texture.width * textureScale;
                    float yCoord = yShift + (float)y / (float)texture.height * textureScale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pointColor = new Color(sample, sample, sample);
                    texture.SetPixel(x, y, pointColor);
                }
            }
            texture.Apply();
            return texture;
        }
    }


    public int yShift = 0;              // Texture "Y" axis shift 
    public int xShift = 0;              // Texture "X" axis shift 
    public int textureScale = 2;        // Texture scale

    public int worldSize = 500;         // Size of the whole land "X" and "Z"
    public int worldStep = 10;          // Place vert each worldStep value
    public int minHG = 0;               // Minimun hg of the land
    public int maxHG = 100;             // Maximum hg of the land

    /// <summary>
    /// Generate land from hg map
    /// </summary>
    [ExecuteInEditMode]
    public void Generate()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Vector3[] verts = GenerateVerticies();
        Mesh mesh = BuildMesh(verts);
        meshFilter.mesh = mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //meshRenderer.material = new Material(Shader.Find("UniversalRenderPipeline/Lit"));
    }


    /// <summary>
    /// Place verts on the texture (x,y) value
    /// </summary>
    /// <returns>Array of verts</returns>
    [ExecuteInEditMode]
    private Vector3[] GenerateVerticies()
    {
        Texture2D texture = Texture;
        int textureSize = texture.width;
        List<Vector3> verts = new List<Vector3>();
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {

                float texValue = texture.GetPixel(x, y).grayscale * (maxHG - minHG);
                Vector3 vert = new Vector3(y * worldStep, texValue, x * worldStep);
                verts.Add(vert);

            }
        }

        return verts.ToArray();
    }

    [ExecuteInEditMode]
    private Mesh BuildMesh(Vector3[] verts)
    {
        Mesh meshFinal = new Mesh();
        meshFinal.vertices = verts;
        meshFinal.triangles = Triangulate(verts);
        meshFinal.uv = UVs(verts);
        meshFinal.RecalculateNormals();
        meshFinal.RecalculateBounds();

        return meshFinal;
    }
    [ExecuteInEditMode]
    private Vector2[] UVs(Vector3[] verts)
    {
        int textureSize = Texture.width;
        Vector2[] uvs = new Vector2[verts.Length];

        float xOffset = 0;
        float yOffset = 0;
        int yCount = 0;
        int xCount = 0;
        for (int i = 0; i < verts.Length; i++)
        {
            if (i != 0 && (i + 1) % textureSize == 0)
            {
                xOffset = 0;
                yCount++;
                xCount = 0;
                yOffset += yCount / textureSize - 1;
            }
            uvs[i] = new Vector2(xOffset, yOffset);
            xCount++;
            xOffset += xCount / textureSize - 1;
        }


        return uvs;
    }
    [ExecuteInEditMode]
    private int[] Triangulate(Vector3[] verts)
    {
        List<int> trisList = new List<int>();
        int textureSize = Texture.width;
        for (int i = 0; i < verts.Length; i++)
        {
            //Build first tris
            if (i < verts.Length - textureSize)
            {
                if ((i + 1) % textureSize == 0 && i != 0)
                {
                    continue;
                }
                trisList.Add(i);
                trisList.Add(i + 1);
                int nextRowIndex = i + textureSize;
                trisList.Add(nextRowIndex);
                //build flipped tris
                trisList.Add(i + 1);
                trisList.Add(nextRowIndex + 1);
                trisList.Add(nextRowIndex);
            }
            else
            {
                break;
            }
        }
        return trisList.ToArray();
    }

}
