using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
	public Ball Ball { get; set; }
	//public GameObject World { }

	public bool GameStarted = false;
	public bool GameEnded = false;

	public Cubes Cubes;

	public UIManager UIManager;

	public float StartingTimeUntilGameOver = 3;
	public float TimeUntilGameOver = 3;

	public eManagerStatus Status { get; set; }

	public void Startup()
	{
		Status = eManagerStatus.Started;
		ResetTimeUntilGameOver();
	}

	private void Update()
	{
		if(TimeUntilGameOver <= 0)
		{
			GameOver();
		}
		else if(GameStarted && !GameEnded)
		{
			TimeUntilGameOver -= Time.deltaTime;
		}
		if(!GameStarted && !GameEnded)
		{
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			{
				StartGame();
			}
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Restart();
		}
	}

	public void ResetTimeUntilGameOver()
	{
		TimeUntilGameOver = StartingTimeUntilGameOver;
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
		GameEnded = true;
		UIManager.FadeInWinScreen();
	}

	public void GameOver()
	{
		GameEnded = true;
		UIManager.FadeInLoseScreen();
	}

	public void Restart()
	{
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevel()
	{
		Debug.Log("dsa");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void StartGame()
	{
		foreach (Cube cube in Cubes.GetComponentsInChildren<Cube>())
		{
			cube.SelectedSprite.SetActive(false);
		}
		ResetTimeUntilGameOver();
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
