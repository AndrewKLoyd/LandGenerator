using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LandGen))]
public class LandGenEditor : Editor
{
    private LandGen landGen;
    public override void OnInspectorGUI()
    {
        if (landGen == null) landGen = target as LandGen;
        GenerateMainView();
    }
    private void GenerateMainView()
    {
        GUILayout.Box(landGen.Texture);

        EditorGUILayout.LabelField("Texture settings");
        EditorGUILayout.LabelField("X coord shift", landGen.xShift.ToString());
        EditorGUILayout.LabelField("Y coord shift", landGen.yShift.ToString());
        EditorGUILayout.LabelField("Texture scale", landGen.textureScale.ToString());

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("Land settings");
        EditorGUILayout.LabelField("World step", landGen.worldStep.ToString());
        EditorGUILayout.LabelField("World size", landGen.worldSize.ToString());
        EditorGUILayout.LabelField("Minimum height", landGen.minHG.ToString());
        EditorGUILayout.LabelField("Maximum height", landGen.maxHG.ToString());
        if (GUILayout.Button("Open editor window")) { LandGenWindow.ShowWin(); }
    }
}
