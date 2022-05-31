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

    private void OnGUI()
    {
        maxSize = new Vector2(256, maxSize.y);
        minSize = new Vector2(256, 500);

        if (GUILayout.Button("New land")) GenerateNewLand();
        landGen = Selection.activeGameObject?.GetComponent<LandGen>();
        if (landGen == null) return;

        GUILayout.Box(landGen.Texture);

        EditorGUILayout.LabelField("Texture settings");
        landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);

        EditorGUILayout.Space(50);

        EditorGUILayout.LabelField("Land settings");
        landGen.worldSize = EditorGUILayout.IntField("World size", landGen.worldSize);
        EditorGUILayout.LabelField("World step", landGen.WorldStep.ToString());
        landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);

        if (GUILayout.Button("Apply land")) ApplyLand();
        if (GUILayout.Button("Remove land")) RemoveLand();
        if (GUI.changed) landGen.RegenerateTexture();
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
