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
        ////GUI.DrawTexture(new Rect(0, 0, 256, 256), landGen.Texture);

        //EditorGUILayout.LabelField("Texture settings");
        //landGen.xShift = EditorGUILayout.IntField("X coord shift", landGen.xShift);
        //landGen.yShift = EditorGUILayout.IntField("Y coord shift", landGen.yShift);
        //landGen.textureScale = EditorGUILayout.IntField("Texture scale", landGen.textureScale);

        //EditorGUILayout.Space(50);

        //EditorGUILayout.LabelField("Land settings");
        //landGen.minHG = EditorGUILayout.IntField("Minimum height", landGen.minHG);
        //landGen.maxHG = EditorGUILayout.IntField("Maximum height", landGen.maxHG);
    }
}
