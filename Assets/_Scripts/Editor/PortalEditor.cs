using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Portal))]
public class PortalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Portal portal = (Portal)target;

        if (GUILayout.Button("Toggle open state"))
        {
            portal.ChangeOpenState(true);
            // EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }

        if (GUILayout.Button("Update Portal Colors"))
        {
            portal.UpdatePortalColors();
        }
    }
}