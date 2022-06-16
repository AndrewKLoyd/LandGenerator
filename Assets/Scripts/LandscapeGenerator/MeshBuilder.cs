using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGenerator
{
    public static class MeshBuilder
    {
        //public static Mesh GenerateMesh(NoiseParams noise)
        //{
        //    float step = worldSize / Texture.width;
        //    //Vector3[] verts = null;
        //    //Vector2[] uvs = null;
        //    return null;
        //}

        ///// <summary>
        ///// Place verts on the texture (x,y) value
        ///// </summary>
        ///// <returns>Array of verts</returns>
        //private static Vector3[] GenerateVerticies()
        //{
        //    Texture2D texture = Texture;
        //    int worldStep = WorldStep;
        //    int textureSize = texture.width;
        //    List<Vector3> verts = new List<Vector3>();
        //    for (int y = 0; y < texture.height; y++)
        //    {
        //        for (int x = 0; x < texture.width; x++)
        //        {

        //            float texValue = texture.GetPixel(x, y).grayscale * (maxHG - minHG);
        //            Vector3 vert = new Vector3(y * worldStep, texValue, x * worldStep);
        //            verts.Add(vert);
        //        }
        //    }

        //    return verts.ToArray();
        //}

        //private static Mesh BuildMesh(Vector3[] verts)
        //{
        //    Mesh meshFinal = new Mesh();
        //    meshFinal.vertices = verts;
        //    meshFinal.triangles = Triangulate(verts);
        //    meshFinal.uv = UVs(verts);
        //    meshFinal.RecalculateNormals();
        //    meshFinal.RecalculateBounds();

        //    return meshFinal;
        //}
        //private static Vector2[] UVs(Vector3[] verts)
        //{
        //    int textureSize = Texture.width;
        //    Vector2[] uvs = new Vector2[verts.Length];

        //    float xOffset = 0;
        //    float yOffset = 0;
        //    int yCount = 0;
        //    int xCount = 0;
        //    for (int i = 0; i < verts.Length; i++)
        //    {
        //        if (i != 0 && (i + 1) % textureSize == 0)
        //        {
        //            xOffset = 0;
        //            yCount++;
        //            xCount = 0;
        //            yOffset += yCount / textureSize - 1;
        //        }
        //        uvs[i] = new Vector2(xOffset, yOffset);
        //        xCount++;
        //        xOffset += xCount / textureSize - 1;
        //    }


        //    return uvs;
        //}
        //private static int[] Triangulate(Vector3[] verts)
        //{
        //    List<int> trisList = new List<int>();
        //    int textureSize = Texture.width;
        //    for (int i = 0; i < verts.Length; i++)
        //    {
        //        //Build first tris
        //        if (i < verts.Length - textureSize)
        //        {
        //            if ((i + 1) % textureSize == 0 && i != 0)
        //            {
        //                continue;
        //            }
        //            trisList.Add(i);
        //            trisList.Add(i + 1);
        //            int nextRowIndex = i + textureSize;
        //            trisList.Add(nextRowIndex);
        //            //build flipped tris
        //            trisList.Add(i + 1);
        //            trisList.Add(nextRowIndex + 1);
        //            trisList.Add(nextRowIndex);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    return trisList.ToArray();
        //}
    }
}