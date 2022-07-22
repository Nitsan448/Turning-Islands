using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeFaceBuilder))]
public class CubeFaceBuilderEditor : Editor
{
    private static bool ShowPortalFoldout = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CubeFaceBuilder cubeFaceBuilder = (CubeFaceBuilder)target;
        
        ShowPortalFoldout = EditorGUILayout.Foldout(ShowPortalFoldout, "Portal");
		if (ShowPortalFoldout)
		{
            if (GUILayout.Button("Create Portal"))
            {
                cubeFaceBuilder.CreatePortal();
            }
        }
    }
}
