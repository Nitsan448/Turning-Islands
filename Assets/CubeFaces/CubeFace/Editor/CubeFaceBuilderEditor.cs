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
        CubeFaceBuilder CubeFaceBuilder = (CubeFaceBuilder)target;
        Transform parentCube = CubeFaceBuilder.transform.parent;

        ChangeCubeFaceSection(CubeFaceBuilder);
        GUILayout.Space(10);

        SelectCubeFaceSection(parentCube);
        GUILayout.Space(10);

        SelectNeighborCubeSection(parentCube);
    }

    public void ChangeCubeFaceSection(CubeFaceBuilder cubeFaceBuilder)
    {
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
            cubeFaceBuilder.CreateBouncySurface(true);
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

    public void SelectCubeFaceSection(Transform parentCube)
    {
        GUILayout.Label("Select a cube face", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Top"))
        {
            Selection.activeObject = parentCube.GetChild(0);
        }

        if (GUILayout.Button("Right"))
        {
            Selection.activeObject = parentCube.GetChild(1);
        }

        if (GUILayout.Button("Bottom"))
        {
            Selection.activeObject = parentCube.GetChild(2);
        }

        if (GUILayout.Button("Left"))
        {
            Selection.activeObject = parentCube.GetChild(3);
        }

        GUILayout.EndHorizontal();
    }

    public void SelectNeighborCubeSection(Transform parentCube)
    {
        GUILayout.Label("Select a neighbor cube", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Top") && parentCube.GetComponent<Cube>().TopCube)
        {
            Selection.activeObject = parentCube.GetComponent<Cube>().TopCube.gameObject;
        }

        if (GUILayout.Button("Right") && parentCube.GetComponent<Cube>().RightCube)
        {
            Selection.activeObject = parentCube.GetComponent<Cube>().RightCube.gameObject;
        }

        if (GUILayout.Button("Bottom") && parentCube.GetComponent<Cube>().BottomCube)
        {
            Selection.activeObject = parentCube.GetComponent<Cube>().BottomCube.gameObject;
        }

        if (GUILayout.Button("Left") && parentCube.GetComponent<Cube>().LeftCube)
        {
            Selection.activeObject = parentCube.GetComponent<Cube>().LeftCube.gameObject;
        }

        GUILayout.EndHorizontal();
    }
}