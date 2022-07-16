using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube rightCube;
    [SerializeField] private Cube leftCube;
    [SerializeField] private Cube upCube;
    [SerializeField] private Cube downCube;

	[SerializeField] private float _rotationTime = 0.5f;

	private BouncyObject[] BouncyObjects;

    public bool Selected;

	private void Awake()
	{
		BouncyObjects = GetComponentsInChildren<BouncyObject>();
	}

	void Update()
    {
		if (Selected)
		{
			UpdateSelectedCubesState();
			UpdateCubeRotation();
		}
    }

    private void UpdateSelectedCubesState()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpdateSelectedCube(upCube);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			UpdateSelectedCube(rightCube);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			UpdateSelectedCube(leftCube);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			UpdateSelectedCube(downCube);
		}
	}

	private void UpdateSelectedCube(Cube cubeToSelect)
	{
		if(cubeToSelect != null)
		{
			Selected = false;
			cubeToSelect.Selected = true;
		}
	}

    private void UpdateCubeRotation()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(UpdateCubeRotationCoroutine(eDirection.Right));
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			StartCoroutine(UpdateCubeRotationCoroutine(eDirection.Left));
		}
	}

    private IEnumerator UpdateCubeRotationCoroutine(eDirection direction)
	{
		Quaternion currentRotation = transform.rotation;
		Quaternion targetRotation;
		if(direction == eDirection.Left)
		{
			targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
													(transform.eulerAngles.z - 90) % 360);

		}
		else
		{
			targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
														(transform.eulerAngles.z + 90) % 360);

		}
		float currentTime = 0;

		while(currentTime < _rotationTime)
		{
			transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, currentTime / _rotationTime);
			currentTime += Time.deltaTime;
			yield return null;
		}

		transform.rotation = targetRotation;

		foreach(BouncyObject bouncyObject in BouncyObjects)
		{
			Debug.Log(bouncyObject);
			bouncyObject.UpdateDirection(direction);
		}

		yield return null;
	}
}
