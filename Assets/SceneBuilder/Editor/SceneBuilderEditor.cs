using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneBuilder))]
public class SceneBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Build Scene"))
        {
            (target as SceneBuilder).BuildScene();
        }

        if (GUILayout.Button("Change Cubes Positions"))
        {
            (target as SceneBuilder).ChangeCubesPositions();
        }

        if (GUILayout.Button("Connect Cubes"))
        {
            (target as SceneBuilder).ConnectCubes(true);
        }
    }
}