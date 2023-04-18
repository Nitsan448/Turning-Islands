using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeFaceBuilder))]
public class CubeFaceBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CubeFaceBuilder cubeFaceBuilder = (CubeFaceBuilder)target;

        if (GUILayout.Button("Create Portal"))
        {
            cubeFaceBuilder.CreatePortal();
        }
        if (GUILayout.Button("Create Portal Button"))
        {
            cubeFaceBuilder.CreatePortalButton();
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
        if (GUILayout.Button("Create Right Trampoline"))
        {
            cubeFaceBuilder.CreateTrampoline(eDirection.Right);
        }
        if (GUILayout.Button("Create Left Trampoline"))
        {
            cubeFaceBuilder.CreateTrampoline(eDirection.Left);
        }
        if (GUILayout.Button("Create Ball"))
        {
            cubeFaceBuilder.CreateBall();
        }
    }
}
