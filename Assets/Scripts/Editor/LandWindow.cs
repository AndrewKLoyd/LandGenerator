using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LandGenerator
{

    [RequireComponent(typeof(Land))]
    public class LandWindow : EditorWindow
    {
        [MenuItem("Window/Land generator")]
        public static void ShowWin() => GetWindow<LandWindow>("Land");


        private Vector2 scrollPose = Vector2.zero;
        private GeneratorType currGenerator;
        private Land currLand;

        private void OnGUI()
        {
            maxSize = new Vector2(256, maxSize.y);
            minSize = new Vector2(256, 500);

            scrollPose = EditorGUILayout.BeginScrollView(scrollPose);

            currLand = Selection.activeGameObject?.GetComponent<Land>();
            if (currLand == null)
            {
                if (GUILayout.Button("New land"))
                {
                    CreateNewLand();
                }
                EditorGUILayout.EndScrollView();
                return;
            }

            currGenerator = (GeneratorType)EditorGUI.EnumPopup(new Rect(new Vector2(0, 2), new Vector2(position.width, 20)), currGenerator);
            switch (currGenerator)
            {
                case GeneratorType.Terrain:
                    DrawTerrainEditor();
                    break;
                case GeneratorType.Textures:
                    DrawTexturesEditor();
                    break;
                case GeneratorType.Enviroment:
                    DrawEnviromentEditor();
                    break;
            }
            EditorGUILayout.EndScrollView();
        }
        private void CreateNewLand()
        {
            GameObject land = new GameObject("Land");
            land.transform.position = Vector3.zero;
            land.transform.rotation = Quaternion.Euler(Vector3.zero);
            land.AddComponent<Land>();
            Selection.activeGameObject = land;
        }
        private void DrawTerrainEditor() { }
        private void DrawTexturesEditor() { }
        private void DrawEnviromentEditor() { }

    }

}