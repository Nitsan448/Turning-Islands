#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class SceneBuilder : MonoBehaviour
{
    [SerializeField]
    private int _numberOfRows;

    [SerializeField]
    private int _numberOfColumns;

    [SerializeField]
    private Vector2 _distanceBetweenCubes = new Vector2(8, 8);

    private string _sceneBuilderPrefabsPath = "Assets/Prefabs/SceneBuilder/";
    private GameObject _cubesManager;
    private GameObject _managers;
    private GameObject _ui;
    private GameObject _mainCamera;
    private GameObject _background;
    private GameObject _globalLight;
    private GameObject _cube;
    private GameObject _musicPlayer;

    public Cube[,] Cubes;

    public void BuildScene()
    {
        LoadAllPrefabs();
        InstantiateSingletons();
        InstantiateCubes();
        ConnectCubes();
        HandleReferences();
    }

    private void LoadAllPrefabs()
    {
        _managers = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/Managers.prefab"
        );
        _ui = AssetDatabase.LoadAssetAtPath<GameObject>(_sceneBuilderPrefabsPath + "/UI.prefab");
        _mainCamera = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/Main Camera.prefab"
        );
        _background = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/Background.prefab"
        );
        _globalLight = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/GlobalLight.prefab"
        );
        _cubesManager = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/CubesManager.prefab"
        );
        _cube = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/Cube.prefab"
        );
        _musicPlayer = AssetDatabase.LoadAssetAtPath<GameObject>(
            _sceneBuilderPrefabsPath + "/MusicPlayer.prefab"
        );
    }

    private void InstantiateSingletons()
    {
        _managers = PrefabUtility.InstantiatePrefab(_managers) as GameObject;
        _ui = PrefabUtility.InstantiatePrefab(_ui) as GameObject;
        _mainCamera = PrefabUtility.InstantiatePrefab(_mainCamera) as GameObject;
        _background = PrefabUtility.InstantiatePrefab(_background) as GameObject;
        _cubesManager = PrefabUtility.InstantiatePrefab(_cubesManager) as GameObject;
        _cubesManager.GetComponent<CubesManager>().DistanceBetweenCubes = _distanceBetweenCubes;
        _globalLight = PrefabUtility.InstantiatePrefab(_globalLight) as GameObject;
        _musicPlayer = PrefabUtility.InstantiatePrefab(_musicPlayer) as GameObject;
    }

    private void InstantiateCubes()
    {
        Cubes = new Cube[_numberOfRows, _numberOfColumns];
        Vector2 topLeftCubePosition = GetTopLeftCubePosition();

        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                GameObject cube = PrefabUtility.InstantiatePrefab(_cube) as GameObject;
                Cubes[row, column] = cube.GetComponent<Cube>();
                SetCubePosition(row, column, topLeftCubePosition);
                cube.transform.parent = _cubesManager.transform;
                cube.name = "Cube: " + (row + 1) + ", " + (column + 1);
            }
        }
    }

    private void SetCubePosition(int cubeRow, int cubeColumn, Vector2 topLeftCubePosition)
    {
        float cubeXPosition = topLeftCubePosition.x + _distanceBetweenCubes.x * cubeColumn;
        float cubeYPosition = topLeftCubePosition.y - _distanceBetweenCubes.y * cubeRow;
        Cubes[cubeRow, cubeColumn].transform.position = new Vector2(cubeXPosition, cubeYPosition);
    }

    public void ChangeCubesPositions()
    {
        Vector2 topLeftCubePosition = GetTopLeftCubePosition();
        FindObjectOfType<CubesManager>().DistanceBetweenCubes = _distanceBetweenCubes;

        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                SetCubePosition(row, column, topLeftCubePosition);
            }
        }
    }

    private Vector2 GetTopLeftCubePosition()
    {
        Vector2 topLeftCubePosition;
        topLeftCubePosition = new Vector2(
            -(_distanceBetweenCubes.x * (_numberOfColumns / 2)),
            _distanceBetweenCubes.y * (_numberOfRows / 2)
        );

        if (_numberOfColumns % 2 == 0)
        {
            topLeftCubePosition.x += _distanceBetweenCubes.x / 2;
        }
        if (_numberOfRows % 2 == 0)
        {
            topLeftCubePosition.y -= _distanceBetweenCubes.y / 2;
        }

        return topLeftCubePosition;
    }

    private void ConnectCubes()
    {
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int column = 0; column < _numberOfColumns; column++)
            {
                ConnectWithLeftCube(row, column);
                ConnectWithRightCube(row, column);
                ConnectWithTopCube(row, column);
                ConnectWithBottomCube(row, column);
            }
        }
    }

    private void ConnectWithLeftCube(int rowIndex, int columnIndex)
    {
        if (columnIndex > 0)
        {
            Cubes[rowIndex, columnIndex].LeftCube = Cubes[rowIndex, columnIndex - 1];
            Cubes[rowIndex, columnIndex - 1].RightCube = Cubes[rowIndex, columnIndex];
        }
    }

    private void ConnectWithRightCube(int rowIndex, int columnIndex)
    {
        if (columnIndex != _numberOfColumns - 1)
        {
            Cubes[rowIndex, columnIndex].RightCube = Cubes[rowIndex, columnIndex + 1];
            Cubes[rowIndex, columnIndex + 1].LeftCube = Cubes[rowIndex, columnIndex];
        }
    }

    private void ConnectWithBottomCube(int rowIndex, int columnIndex)
    {
        if (rowIndex != _numberOfRows - 1)
        {
            Cubes[rowIndex, columnIndex].BottomCube = Cubes[rowIndex + 1, columnIndex];
            Cubes[rowIndex + 1, columnIndex].TopCube = Cubes[rowIndex, columnIndex];
        }
    }

    private void ConnectWithTopCube(int rowIndex, int columnIndex)
    {
        if (rowIndex != 0)
        {
            Cubes[rowIndex, columnIndex].TopCube = Cubes[rowIndex - 1, columnIndex];
            Cubes[rowIndex - 1, columnIndex].BottomCube = Cubes[rowIndex, columnIndex];
        }
    }

    private void HandleReferences()
    {
        _cubesManager.GetComponent<CubesManager>().SelectedCube = Cubes[0, 0];
        _mainCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D =
            _background.GetComponent<PolygonCollider2D>();
    }
}
#endif