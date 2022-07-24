using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeFaceBuilder))]
public class CubeFaceBuilderEditor : Editor
{
    //private static bool ShowPortalFoldout = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CubeFaceBuilder cubeFaceBuilder = (CubeFaceBuilder)target;
        
        if (GUILayout.Button("Create Portal"))
        {
            cubeFaceBuilder.CreatePortal();
        }
        if (GUILayout.Button("Create Bouncy Surface"))
        {
            cubeFaceBuilder.CreateBouncySurface();
        }
        if (GUILayout.Button("Create Turned Tube"))
        {
            cubeFaceBuilder.CreateTube(true, true);
        }
        if (GUILayout.Button("Create Straight Tube"))
        {
            cubeFaceBuilder.CreateTube(true, false);
        }
        if (GUILayout.Button("Create Magnet"))
        {
            cubeFaceBuilder.CreateMagnet();
        }
        if (GUILayout.Button("Create Win Flag"))
        {
            cubeFaceBuilder.CreateWinFlag();
        }
        if (GUILayout.Button("Create Ball"))
        {
            cubeFaceBuilder.CreateBall();
        }
    }
}
