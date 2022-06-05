using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LandGen))]
public class LandGenWindow : EditorWindow
{
    private LandGen landGen;
    [MenuItem("Window/Landscape generator")]
    public static void ShowWin() => GetWindow<LandGenWindow>("Generator");
    private Vector2 scrollPose = Vector2.zero;


    private void OnGUI()
    {
        scrollPose = EditorGUILayout.BeginScrollView(scrollPose);
        maxSize = new Vector2(256, maxSize.y);
        minSize = new Vector2(256, 500);

        if (GUILayout.Button("New land")) GenerateNewLand();
        landGen = Selection.activeGameObject?.GetComponent<LandGen>();
        if (landGen == null) return;
        GUILayout.Box(landGen.Texture);

        EditorGUILayout.Space(50);
        landGen.invert = EditorGUILayout.Toggle("Invert map", landGen.invert);
        landGen.noiseType = (NoiseType)EditorGUI.EnumPopup(new Rect(5, 256 + 40, 100, 20), landGen.noiseType);
        switch (landGen.noiseType)
        {
            case NoiseType.Perlin:
                LoadPerlinNoiseSetup();
                break;
            case NoiseType.Simplex:
                LoadSimplexNoiseSetup();
                break;
        }

        if (GUILayout.Button("Apply land")) ApplyLand();
        if (GUILayout.Button("Remove land")) RemoveLand();
        if (GUI.changed) landGen.RegenerateTexture();
        EditorGUILayout.EndScrollView();
    }

    private void LoadPerlinNoiseSetup()
    {
        EditorGUILayout.LabelField("Texture settings");
        landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);

        EditorGUILayout.LabelField("Land settings");
        landGen.worldSize = EditorGUILayout.IntField("World size", landGen.worldSize);
        EditorGUILayout.LabelField("World step", landGen.WorldStep.ToString());
        landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);
    }

    private void LoadSimplexNoiseSetup()
    {
        EditorGUILayout.LabelField("Texture settings");
        landGen.seed = EditorGUILayout.LongField("Seed", landGen.seed);
        landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        landGen.zShift = EditorGUILayout.IntField("Z coord shift", landGen.zShift);
        landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);
        landGen.octaves = EditorGUILayout.IntField("Octaves", landGen.octaves);
        landGen.divide = EditorGUILayout.FloatField("Divide", landGen.divide);

        landGen.worldSize = EditorGUILayout.IntField("World size", landGen.worldSize);
        EditorGUILayout.LabelField("World step", landGen.WorldStep.ToString());
        landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);
    }
    private void GenerateNewLand()
    {
        GameObject go = new GameObject("LandGen");
        landGen = go.AddComponent<LandGen>();
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
