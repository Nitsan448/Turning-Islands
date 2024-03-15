using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour, IGameManager
{
    public event Action GameStarted;
    public eGameState GameState { get; private set; } = eGameState.Editing;
    private float _timeUntilLevelEndedScreen = 0.5f;

    public int ballsNotInFlag = 0;

    public void Startup()
    {
    }

    private void Update()
    {
        HandleSpaceAndReturnInputs();
        HandleRestartInput();
    }

    private void HandleSpaceAndReturnInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (GameState == eGameState.Editing)
            {
                StartGame();
            }
            else if (GameState == eGameState.GameEnded)
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
        Managers.UI.Invoke("FadeInWinScreen", _timeUntilLevelEndedScreen);
    }

    public void GameOver()
    {
        GameState = eGameState.GameEnded;
        Managers.UI.Invoke("FadeInLoseScreen", _timeUntilLevelEndedScreen);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel", 1) >= SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex + 1);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        GameState = eGameState.GameRunning;
        Managers.Cubes.SelectedCube.SelectedSprite.SetActive(false);
        GameStarted?.Invoke();
        ChangeToGameCamera();
    }

    private void ChangeToGameCamera()
    {
        Camera.main.GetComponent<FreeCamera>().enabled = false;
        Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
}