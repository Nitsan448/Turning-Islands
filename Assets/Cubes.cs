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
		}
	}

	void Update()
    {
		UpdateSelectedCubesState();
		foreach(Cube cube in GetComponentsInChildren<Cube>())
		{
			if (cube.Selected)
			{
				_selectedCube = cube;
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
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
			cubeToSelect.Selected = true;
			_selectedCube = cubeToSelect;
		}
	}
}
