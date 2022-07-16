using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour
{
	private Cube _selectedCube;

	private void Awake()
	{
		foreach(Cube cube in GetComponentsInChildren<Cube>())
		{
			cube.GetComponent<PointEffector2D>().enabled = false;
			if (cube.Selected)
			{
				_selectedCube = cube;
			}
		}
	}

	void Update()
    {
		if (!Managers.GameManager.GameStarted)
		{
			UpdateSelectedCubesState();
		}
		foreach(Cube cube in GetComponentsInChildren<Cube>())
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Managers.GameManager.StartGame();
				cube.SelectedSprite.SetActive(false);
				cube.GetComponent<PointEffector2D>().enabled = true;
			}
		}
	}
	private void UpdateSelectedCubesState()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpdateSelectedCube(_selectedCube.upCube);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			UpdateSelectedCube(_selectedCube.rightCube);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			UpdateSelectedCube(_selectedCube.leftCube);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			UpdateSelectedCube(_selectedCube.downCube);
		}
	}

	private void UpdateSelectedCube(Cube cubeToSelect)
	{
		if (cubeToSelect != null)
		{
			_selectedCube.Selected = false;
			_selectedCube.SelectedSprite.SetActive(false);
			_selectedCube = cubeToSelect;
			cubeToSelect.Selected = true;
			cubeToSelect.SelectedSprite.SetActive(true);
		}
	}
}
