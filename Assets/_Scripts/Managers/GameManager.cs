using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    public eDirection GravityDirection = eDirection.Down;
	public Ball Ball { get; set; }
	//public GameObject World { }

	public bool GameStarted = false;

	public Cubes Cubes;

	public eManagerStatus Status { get; set; }

	public void Startup()
	{
		Status = eManagerStatus.Started;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void ChangeEffectorsState(bool newState)
	{
		foreach (Cube cube in Cubes.gameObject.GetComponentsInChildren<Cube>())
		{
			cube.GetComponent<PointEffector2D>().enabled = newState;
		}
	}

	public void LevelWon()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log("Level won");
	}

	public void GameOver()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void StartGame()
	{
		Managers.GameManager.GameStarted = true;
		Camera.main.GetComponent<Camera2D>().enabled = false;
		Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
		ChangeEffectorsState(true);
		//StartCoroutine(LerpCamera());
	}

	private IEnumerator LerpCamera()
	{
		Camera camera = Camera.main;
		Vector3 startingPosition = camera.transform.position;
		Vector3 targetPosition = Ball.transform.position;
		float currentTime = 0;

		while(currentTime < 1)
		{
			camera.transform.position = Vector3.Lerp(startingPosition, targetPosition, currentTime / 1);
			currentTime += Time.deltaTime;
			yield return null;
		}
		camera.transform.position = targetPosition;

		camera.GetComponent<CinemachineVirtualCamera>().enabled = true;
	}
}
