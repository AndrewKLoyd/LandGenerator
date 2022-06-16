using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace TerrainGenerator
{
    [CustomEditor(typeof(LandGenerator))]
    public class LandGenEditor : Editor
    {
        private LandGenerator landGen;
        public override void OnInspectorGUI()
        {
            if (landGen == null) landGen = target as LandGenerator;
            GenerateMainView();
        }
        private void GenerateMainView()
        {
            if (landGen == null || landGen.landParams == null || landGen.noiseParams == null) return;
            GUILayout.Box(landGen.noiseParams.Texture);

            EditorGUILayout.LabelField("Texture settings");
            EditorGUILayout.LabelField("Is inverted: ", landGen.noiseParams.Invert.ToString());
            EditorGUILayout.LabelField("X coord shift: ", landGen.noiseParams.XShift.ToString());
            EditorGUILayout.LabelField("Y coord shift: ", landGen.noiseParams.YShift.ToString());
            EditorGUILayout.LabelField("Frequency: ", landGen.noiseParams.Frequency.ToString());
            EditorGUILayout.LabelField("Octaves: ", landGen.noiseParams.Octaves.ToString());
            EditorGUILayout.LabelField("Redistribution: ", landGen.noiseParams.Redistribution.ToString());

            GUILayout.Space(20);

            EditorGUILayout.LabelField("Land settings");
            EditorGUILayout.LabelField("Map size: ", landGen.landParams.WorldSize.ToString());
            EditorGUILayout.LabelField("Min height: ", landGen.landParams.MinHG.ToString());
            EditorGUILayout.LabelField("Max height: ", landGen.landParams.MaxHG.ToString());
            EditorGUILayout.LabelField("Step: ", landGen.landParams.WorldStep.ToString());

            if (GUILayout.Button("Open editor window")) { LandGenWindow.ShowWin(); }
        }
    }
}