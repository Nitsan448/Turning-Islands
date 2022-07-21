using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
	public CubesManager Cubes { get; set; }
	public eGameState GameState { get; private set; } = eGameState.Editing;

	private float _startingTimeUntilGameOver = 3;
	private float _timeUntilGameOver= 3;
	private float _timeUntilLevelEndedScreen = 0.5f;

	public void Startup()
	{
		ResetTimeUntilGameOver();
	}

	private void Update()
	{
		CheckIfGameEnded();
		if(GameState == eGameState.GameRunning)
		{
			_timeUntilGameOver -= Time.deltaTime;
		}
		HandleSpaceAndReturnInputs();
		HandleRestartInput();
	}

	private void CheckIfGameEnded()
	{
		if (_timeUntilGameOver <= 0)
		{
			GameOver();
		}
	}

	private void HandleSpaceAndReturnInputs()
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
		{
			if(GameState == eGameState.Editing)
			{
				StartGame();
			}
			else if(GameState == eGameState.GameEnded)
			{
				GoToNextLevel();
			}
		}
	}

	private void HandleRestartInput()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}
	}

	public void LevelWon()
	{
		GameState = eGameState.GameEnded;
		Managers.Cubes.ChangeEffectorsState(false);
		Managers.UI.Invoke("FadeInWinScreen", _timeUntilLevelEndedScreen);
	}

	public void GameOver()
	{
		GameState = eGameState.GameEnded;
		Managers.Cubes.ChangeEffectorsState(false);
		Managers.UI.Invoke("FadeInLoseScreen", _timeUntilLevelEndedScreen);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void StartGame()
	{
		GameState = eGameState.GameRunning;

		Managers.Cubes.SelectedCube.SelectedSprite.SetActive(false);

		ResetTimeUntilGameOver();
		ChangeToGameCamera();
		Managers.Cubes.ChangeEffectorsState(true);
	}

	private void ChangeToGameCamera()
	{
		Camera.main.GetComponent<FreeCamera>().enabled = false;
		Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
	}

	public void ResetTimeUntilGameOver()
	{
		_timeUntilGameOver = _startingTimeUntilGameOver;
	}
}
