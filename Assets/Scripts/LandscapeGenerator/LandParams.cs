using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerrainGenerator
{
    public class LandParams
    {
        #region Vars
        private List<Mesh> meshes;
        private int size = 1000;
        private int minHG = 0;
        private int maxHG = 500;
        private float worldStep = 1;
        #endregion

        #region Props

        public int WorldSize
        {
            get => size;
            set
            {
                if (value > 100) size = value;
            }
        }

        public int MinHG
        {
            get => minHG;
            set
            {
                minHG = value < maxHG ? value : minHG;
            }
        }

        public int MaxHG
        {
            get => maxHG;
            set
            {
                maxHG = value > minHG ? value : maxHG;
            }
        }


        public float WorldStep
        {
            get => worldStep;
            set
            {
                worldStep = value > 1 ? value : 1;
            }
        }
        #endregion

        #region Ctors

        public LandParams()
        {
            meshes = new List<Mesh>();
        }

        #endregion

        #region Methods

        #region StaticMethods
        #endregion

        #region PrivateMethods

        private void BuildMeshes(Texture2D texture)
        {
            Vector3[] verts = GetVerts(texture);
            List<List<Vector3>> splittedVerts = SplitVerts(verts);
            foreach (List<Vector3> i in splittedVerts)
            {
                Mesh mesh = new Mesh();
                Vector3[] vertsArray = i.ToArray();
                mesh.vertices = i.ToArray();
                mesh.normals = i.ToArray();
                mesh.triangles = Triangulate(vertsArray);
                mesh.uv = UVs(vertsArray);
                meshes.Add(mesh);
            }
        }


        private List<List<Vector3>> SplitVerts(Vector3[] verts)
        {
            List<List<Vector3>> result = new List<List<Vector3>>();
            List<Vector3> tempVerts = new List<Vector3>();
            int counter = 0;

            for (int i = 0; i < verts.Length; i++)
            {

                if (counter == 65536)
                {
                    counter = 0;
                    result.Add(tempVerts);
                    tempVerts = new List<Vector3>();
                }
                counter++;
                tempVerts.Add(verts[i]);
            }
            result.Add(tempVerts);
            Debug.Log(result.Count);
            return result;
        }

        private Vector3[] GetVerts(Texture2D texture)
        {
            //TODO: rewrite this
            int worldStep = size / texture.width;
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

        private Vector2[] UVs(Vector3[] verts)
        {
            int textureSize = 256;
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

        private int[] Triangulate(Vector3[] verts)
        {
            List<int> trisList = new List<int>();
            int textureSize = 256;
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

        #endregion

        #region PublicMethods

        public Mesh[] GetMeshes(Texture2D texture)
        {
            if (meshes == null || meshes.Count == 0)
            {
                BuildMeshes(texture);
            }
            return meshes.ToArray();
        }

        #endregion

        #endregion

    }
}