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

        GUILayout.Label("Change cube face", EditorStyles.boldLabel);
        GUILayout.Label("Portals", EditorStyles.boldLabel);
        if (GUILayout.Button("Portal"))
        {
            cubeFaceBuilder.CreatePortal();
        }
        if (GUILayout.Button("Portal Button"))
        {
            cubeFaceBuilder.CreatePortalButton();
        }
        GUILayout.Label("Tubes", EditorStyles.boldLabel);
        if (GUILayout.Button("Straight Tube"))
        {
            cubeFaceBuilder.CreateTube(true, false);
        }
        if (GUILayout.Button("Turned Tube"))
        {
            cubeFaceBuilder.CreateTube(true, true);
        }
        GUILayout.Label("Trampolines", EditorStyles.boldLabel);
        if (GUILayout.Button("Right Trampoline"))
        {
            cubeFaceBuilder.CreateTrampoline(eDirection.Right);
        }
        if (GUILayout.Button("Left Trampoline"))
        {
            cubeFaceBuilder.CreateTrampoline(eDirection.Left);
        }
        GUILayout.Label("Other", EditorStyles.boldLabel);
        if (GUILayout.Button("Bouncy Surface"))
        {
            cubeFaceBuilder.CreateBouncySurface();
        }
        if (GUILayout.Button("Magnet"))
        {
            cubeFaceBuilder.CreateMagnet();
        }
        if (GUILayout.Button("Win Flag"))
        {
            cubeFaceBuilder.CreateWinFlag();
        }
        if (GUILayout.Button("Ball"))
        {
            cubeFaceBuilder.CreateBall();
        }
    }
}
