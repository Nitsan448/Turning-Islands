using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public Cube RightCube;
	public Cube LeftCube;
	public Cube TopCube;
	public Cube BottomCube;

	public GameObject SelectedSprite;
	[SerializeField] private float _rotationTime = 0.5f;

	private bool _coroutineActive = false;
	private CubeFace[] _cubeFaces;

	private void Awake()
	{
		_cubeFaces = GetComponentsInChildren<CubeFace>();
	}

    public void RotateCube(eDirection direction)
	{
		if (!_coroutineActive)
		{
			StartCoroutine(UpdateCubeRotationCoroutine(direction));
		}
	}

    private IEnumerator UpdateCubeRotationCoroutine(eDirection direction)
	{
		_coroutineActive = true;
		Managers.Audio.PlaySound("CubeRotation");
		Quaternion currentRotation = transform.rotation;
		Quaternion targetRotation = getTargetRotation(direction);
		float currentTime = 0;

		while(currentTime < _rotationTime)
		{
			transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, currentTime / _rotationTime);
			currentTime += Time.deltaTime;
			yield return null;
		}

		transform.rotation = targetRotation;
		UpdateCubeFacesDirection(direction);

		_coroutineActive = false;
		yield return null;
	}

	private Quaternion getTargetRotation(eDirection direction)
	{
		int zRotationIncrement = 90;
		if (direction == eDirection.Right)
		{
			zRotationIncrement = -90;
		}
		return Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
								(transform.eulerAngles.z + zRotationIncrement) % 360);
	}

	private void UpdateCubeFacesDirection(eDirection direction)
	{
		foreach (CubeFace cubeFace in _cubeFaces)
		{
			cubeFace.UpdateDirection(direction);
		}
	}

	public GameObject GetCubeFaceObjectByDirection(eDirection direction)
	{
		foreach(CubeFace cubeFace in GetComponentsInChildren<CubeFace>())
		{
			if(direction == cubeFace.Direction)
			{
				return cubeFace.gameObject;
			}
		}
		return null;
	}
}
