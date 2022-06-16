using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGenerator
{
    [ExecuteInEditMode]
    public class LandGenerator : MonoBehaviour
    {
        public NoiseParams noiseParams;
        public LandParams landParams;

        private void Awake()
        {
            noiseParams = new NoiseParams();
            landParams = new LandParams();
        }
        [ExecuteInEditMode]
        public void Generate()
        {
            int counter = 0;
            foreach (Mesh mesh in landParams.GetMeshes(noiseParams.Texture))
            {
                GameObject go = new GameObject("Land" + counter.ToString());
                MeshFilter mf = go.AddComponent<MeshFilter>();
                mf.mesh = mesh;
                MeshRenderer mr = go.AddComponent<MeshRenderer>();
                MeshCollider mc = go.AddComponent<MeshCollider>();
                mc.sharedMesh = mesh;
                go.transform.SetParent(transform);
                counter++;
            }
        }
    }
}