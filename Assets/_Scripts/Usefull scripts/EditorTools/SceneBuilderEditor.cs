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

		SceneBuilder sceneBuilder = target as SceneBuilder;
		if(GUILayout.Button("Build scene"))
		{
			sceneBuilder.BuildScene();
		}
	}
}
