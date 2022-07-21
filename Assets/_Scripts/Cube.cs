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

	private bool coroutineActive = false;

	private CubeFace[] cubeFaces;

	private void Awake()
	{
		cubeFaces = GetComponentsInChildren<CubeFace>();
	}

    public void RotateCube(eDirection direction)
	{
		if (!coroutineActive)
		{
			StartCoroutine(UpdateCubeRotationCoroutine(direction));
		}
	}

    private IEnumerator UpdateCubeRotationCoroutine(eDirection direction)
	{
		coroutineActive = true;
		GetComponent<AudioSource>().Play();
		Quaternion currentRotation = transform.rotation;
		Quaternion targetRotation;
		if(direction == eDirection.Left)
		{
			targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
													(transform.eulerAngles.z + 90) % 360);

		}
		else
		{
			targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
														(transform.eulerAngles.z - 90) % 360);

		}
		float currentTime = 0;

		while(currentTime < _rotationTime)
		{
			transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, currentTime / _rotationTime);
			currentTime += Time.deltaTime;
			yield return null;
		}

		transform.rotation = targetRotation;

		foreach(CubeFace cubeFace in cubeFaces)
		{
			cubeFace.UpdateDirection(direction);
		}

		coroutineActive = false;
		yield return null;
	}
}
