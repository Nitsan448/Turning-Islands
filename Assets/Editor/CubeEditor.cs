using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Cube Cube = (Cube)target;

        GUILayout.Label("Select a cube face", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Top"))
        {
            Selection.activeObject = Cube.transform.GetChild(0);
        }
        if (GUILayout.Button("Right"))
        {
            Selection.activeObject = Cube.transform.GetChild(1);
        }
        if (GUILayout.Button("Bottom"))
        {
            Selection.activeObject = Cube.transform.GetChild(2);
        }
        if (GUILayout.Button("Left"))
        {
            Selection.activeObject = Cube.transform.GetChild(3);
        }
        GUILayout.EndHorizontal();
    }
}
