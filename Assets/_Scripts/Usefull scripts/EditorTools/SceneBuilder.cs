using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
	[SerializeField] private List<GameObject> _gameObjects;
	[SerializeField] private List<Vector3> _positions;

    public void BuildScene()
	{
        for(int i = 0; i < _gameObjects.Count; i++)
		{
			Instantiate(_gameObjects[i], _positions[i], Quaternion.identity);
		}
	}
}
