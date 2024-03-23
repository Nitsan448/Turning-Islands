using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Cube Cube = (Cube)target;

        GUILayout.Space(10);

        ChangeCubeFaceSection(Cube);
        GUILayout.Space(10);

        SelectCubeFaceSection(Cube);
        GUILayout.Space(10);

        SelectNeighborCubeSection(Cube);
        GUILayout.Space(10);

        TurnCubeSection(Cube);
        GUILayout.Space(10);

        if (GUILayout.Button("Delete Cube"))
        {
            Cube.DeleteCube();
        }
    }

    private void CreateButtonSection(
        string sectionName,
        string[] buttonLabels,
        Action<int> buttonAction
    )
    {
        GUIStyle centeredText = GUI.skin.GetStyle("Label");
        centeredText.alignment = TextAnchor.UpperCenter;
        centeredText.font = EditorStyles.boldFont;
        GUILayout.Label(sectionName, centeredText);
        GUILayout.BeginHorizontal();
        for (int i = 0; i < buttonLabels.Length; i++)
        {
            if (GUILayout.Button(buttonLabels[i]))
            {
                buttonAction(i);
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(5);
    }

    public void ChangeCubeFaceSection(Cube cube)
    {
        GUILayout.Label("Change cube face", EditorStyles.boldLabel);

        string[] buttonLabels = { "Top", "Right", "Bottom", "Left" };

        CreateButtonSection(
            "Portals",
            buttonLabels,
            childIndex => { cube.transform.GetChild(childIndex).GetComponent<CubeFaceBuilder>().CreatePortal(); }
        );

        CreateButtonSection(
            "Portal buttons",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreatePortalButton();
            }
        );

        CreateButtonSection(
            "Straight Tubes",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTube(true, false);
            }
        );

        CreateButtonSection(
            "Turned Tubes",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTube(true, true);
            }
        );

        CreateButtonSection(
            "Right Trampolines",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTrampoline(eDirection.Right);
            }
        );

        CreateButtonSection(
            "Left Trampolines",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTrampoline(eDirection.Left);
            }
        );

        CreateButtonSection(
            "Bouncy Surfaces",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateBouncySurface(true);
            }
        );

        CreateButtonSection(
            "Cube Turners",
            buttonLabels,
            childIndex =>
            {
                cube.transform
                    .GetChild(childIndex)
                    .GetComponent<CubeFaceBuilder>()
                    .CreateCubeTurner();
            }
        );
                CreateButtonSection(
                    "Magnets",
                    buttonLabels,
                    childIndex =>
                    {
                         cube.transform.GetChild(childIndex).GetComponent<CubeFaceBuilder>().CreateMagnet(); 
                    }
                );

        CreateButtonSection(
            "Win Flags",
            buttonLabels,
            childIndex => { cube.transform.GetChild(childIndex).GetComponent<CubeFaceBuilder>().CreateWinFlag(); }
        );

        CreateButtonSection(
            "Balls",
            buttonLabels,
            childIndex => { cube.transform.GetChild(childIndex).GetComponent<CubeFaceBuilder>().CreateBall(); }
        );
    }

    public void SelectCubeFaceSection(Cube cube)
    {
        GUILayout.Label("Select a cube face", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Top"))
        {
            Selection.activeObject = cube.transform.GetChild(0);
        }

        if (GUILayout.Button("Right"))
        {
            Selection.activeObject = cube.transform.GetChild(1);
        }

        if (GUILayout.Button("Bottom"))
        {
            Selection.activeObject = cube.transform.GetChild(2);
        }

        if (GUILayout.Button("Left"))
        {
            Selection.activeObject = cube.transform.GetChild(3);
        }

        GUILayout.EndHorizontal();
    }

    public void SelectNeighborCubeSection(Cube cube)
    {
        GUILayout.Label("Select a neighbor cube", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Top") && cube.TopCube)
        {
            Selection.activeObject = cube.TopCube.gameObject;
        }

        if (GUILayout.Button("Right") && cube.RightCube)
        {
            Selection.activeObject = cube.RightCube.gameObject;
        }

        if (GUILayout.Button("Bottom") && cube.BottomCube)
        {
            Selection.activeObject = cube.BottomCube.gameObject;
        }

        if (GUILayout.Button("Left") && cube.LeftCube)
        {
            Selection.activeObject = cube.LeftCube.gameObject;
        }

        GUILayout.EndHorizontal();
    }

    public void TurnCubeSection(Cube cube)
    {
        // TODO: change cube faces instead of turning cube
        GUILayout.Label("Turn Cube", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Right"))
        {
            cube.Turn(eDirection.Right);
        }

        // if (GUILayout.Button("Left"))
        // {
        //     cube.Turn(eDirection.Left);
        // }
        GUILayout.EndHorizontal();
    }
}