using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TerrainGenerator
{
    [RequireComponent(typeof(LandGenerator))]
    public class LandGenWindow : EditorWindow
    {
        private LandGenerator landGen;
        [MenuItem("Window/Landscape generator")]
        public static void ShowWin() => GetWindow<LandGenWindow>("Generator");
        private Vector2 scrollPose = Vector2.zero;


        private void OnGUI()
        {

            scrollPose = EditorGUILayout.BeginScrollView(scrollPose);
            maxSize = new Vector2(256, maxSize.y);
            minSize = new Vector2(256, 500);

            if (GUILayout.Button("New land")) GenerateNewLand();
            landGen = Selection.activeGameObject?.GetComponent<LandGenerator>();
            if (landGen != null) LoadUI();

            EditorGUILayout.EndScrollView();
        }

        //private void LoadPerlinNoiseSetup()
        //{
        //    EditorGUILayout.LabelField("Texture settings");
        //    landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        //    landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        //    landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);

        //    EditorGUILayout.LabelField("Land settings");
        //    landGen.worldSize = EditorGUILayout.IntField("World size", landGen.worldSize);
        //    EditorGUILayout.LabelField("World step", landGen.WorldStep.ToString());
        //    landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        //    landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);
        //}

        private void LoadUI()
        {
            GUILayout.Box(landGen.noiseParams.Texture);

            NoiseController();

            EditorGUILayout.Space(20);

            LandController();

            if (GUILayout.Button("Apply land")) ApplyLand();
            if (GUILayout.Button("Remove land")) RemoveLand();
            if (GUI.changed) landGen.noiseParams.RegenerateTexture();
        }

        private void NoiseController()
        {
            landGen.noiseParams.Invert = EditorGUILayout.Toggle("Invert", landGen.noiseParams.Invert);
            landGen.noiseParams.XShift = EditorGUILayout.FloatField("X coord shift", landGen.noiseParams.XShift);
            landGen.noiseParams.YShift = EditorGUILayout.FloatField("Y coord shift", landGen.noiseParams.YShift);
            landGen.noiseParams.Frequency = EditorGUILayout.FloatField("Frequency", landGen.noiseParams.Frequency);
            landGen.noiseParams.Octaves = EditorGUILayout.IntField("Octaves", landGen.noiseParams.Octaves);
            landGen.noiseParams.Redistribution = EditorGUILayout.FloatField("Redistribution", landGen.noiseParams.Redistribution);
        }

        private void LandController()
        {
            landGen.landParams.WorldSize = EditorGUILayout.IntField("World size", landGen.landParams.WorldSize);
            landGen.landParams.MinHG = EditorGUILayout.IntField("Min height", landGen.landParams.MinHG);
            landGen.landParams.MaxHG = EditorGUILayout.IntField("Max height", landGen.landParams.MaxHG);
            landGen.landParams.WorldStep = EditorGUILayout.FloatField("World step", landGen.landParams.WorldStep);
        }

        private void GenerateNewLand()
        {
            GameObject go = new GameObject("LandGenerator");
            landGen = go.AddComponent<LandGenerator>();
            Selection.activeGameObject = go;
        }

        private void ApplyLand()
        {
            landGen.Generate();
        }

        private void RemoveLand()
        {
            DestroyImmediate(landGen.gameObject);
        }


    }
}