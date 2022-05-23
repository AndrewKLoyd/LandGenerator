using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LandGen))]
public class LandGenWindow : EditorWindow
{
    private LandGen landGen;
    [MenuItem("Window/Landscape generator")]
    public static void ShowWin()
    {
        GetWindow<LandGenWindow>("Generator");

    }


    private void OnGUI()
    {
        if (GUILayout.Button("New land")) GenerateNewLand();
        if (landGen == null) return;

        GUILayout.Box(landGen.Texture);
        EditorGUILayout.LabelField("Texture settings");
        landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);

        EditorGUILayout.Space(50);

        EditorGUILayout.LabelField("Land settings");
        landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);
        if (GUILayout.Button("Apply land")) ApplyLand();
    }


    private void GenerateNewLand()
    {
        GameObject go = new GameObject("LandGen");
        landGen = go.AddComponent<LandGen>();
    }

    private void ApplyLand()
    {

    }
}
