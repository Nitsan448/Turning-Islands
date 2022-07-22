using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneBuilder : MonoBehaviour
{
	[SerializeField] private int _numberOfRows;
	[SerializeField] private int _numberOfColumns;
	[SerializeField] private Vector2 _distanceBetweenCubes = new Vector2(8, 8);
	//[SerializeField] private eSceneLayout _sceneLayout;

	private string _sceneBuilderPrefabsPath = "Assets/Prefabs/SceneBuilder/";
	private GameObject _cubesManager;
	private GameObject _managers;
	private GameObject _ui;
	private GameObject _mainCamera;
	private GameObject _background;
	private GameObject _globalLight;
	private GameObject _cube;

	public void BuildScene()
	{
		LoadAllPrefabs();
		InstantiateSingletons();
		InstantiateCubes();
	}

	private void LoadAllPrefabs()
	{
		_managers = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/Managers.prefab");
		_ui = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/UI.prefab");
		_mainCamera = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/Main Camera.prefab");
		_background = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/Background.prefab");
		_globalLight = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/GlobalLight.prefab");
		_cubesManager = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/CubesManager.prefab");
		_cube = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/Cube.prefab");
	}
	
	private void InstantiateSingletons()
	{
		Debug.Log("Instantiating");
		Debug.Log(_managers.name);
		PrefabUtility.InstantiatePrefab(_managers);
		PrefabUtility.InstantiatePrefab(_ui);
		PrefabUtility.InstantiatePrefab(_mainCamera);
		PrefabUtility.InstantiatePrefab(_background);
		_cubesManager = PrefabUtility.InstantiatePrefab(_cubesManager) as GameObject;
		PrefabUtility.InstantiatePrefab(_globalLight);
		//PrefabUtility.InstantiatePrefab(_cube);
	}

	private void InstantiateCubes()
	{
		float topLeftCubeXPosition = -(_distanceBetweenCubes.x * (_numberOfRows / 2));
		float topLeftCubeYPosition = -(_distanceBetweenCubes.y * (_numberOfColumns / 2));
		for(int i = 0; i < _numberOfRows; i++)
		{
			for(int j = 0; j < _numberOfColumns; j++)
			{
				GameObject cube = PrefabUtility.InstantiatePrefab(_cube) as GameObject;
				float cubeXPosition = topLeftCubeXPosition + _distanceBetweenCubes.x * i;
				float cubeYPosition = topLeftCubeXPosition + _distanceBetweenCubes.y * j;
				cube.transform.position = new Vector2(cubeXPosition, cubeYPosition);
				cube.transform.parent = _cubesManager.transform;
			}
		}
	}
}
