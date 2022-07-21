using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeFace))]
public class CubeFaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CubeFace cubeFace = (CubeFace)target;

        if (GUILayout.Button("Create Portal"))
        {
            cubeFace.CreatePortal();
        }
    }
}
