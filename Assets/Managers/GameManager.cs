using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour, IGameManager
{
    public event Action GameStarted;
    public EGameState GameState { get; private set; } = EGameState.Editing;
    private float _timeUntilLevelEndedScreen = 0.5f;
    private Tutorial _tutorial;

    [FormerlySerializedAs("ballsNotInFlag")]
    public int BallsNotInFlag = 0;

    private int _ballsNotInFlagAtStart;

    public void Startup()
    {
        Application.targetFrameRate = 144;
        _ballsNotInFlagAtStart = BallsNotInFlag;
    }

    public void SetStateToTutorial(Tutorial tutorial)
    {
        GameState = EGameState.Tutorial;
        _tutorial = tutorial;
    }

    public void SetStateToEditing()
    {
        GameState = EGameState.Editing;
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
            if (GameState == EGameState.Editing)
            {
                StartGame();
            }
            else if (GameState == EGameState.Tutorial)
            {
                _tutorial.OnTutorialFinished();
            }
            else if (GameState == EGameState.GameEnded)
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

    [Button]
    private void SoftRestart()
    {
        //Add a button to soft restart - restarts but keeps cubes placement
        GameState = EGameState.Editing;
        foreach (Ball ball in FindObjectsOfType<Ball>())
        {
            ball.ResetToStartingState();
        }

        Managers.Cubes.SelectedCube.SelectedSprite.SetActive(true);
        Managers.UI.FadeOutLoseScreen();
        BallsNotInFlag = _ballsNotInFlagAtStart;
    }

    public void LevelWon()
    {
        GameState = EGameState.GameEnded;
        Managers.UI.Invoke("FadeInWinScreen", _timeUntilLevelEndedScreen);
    }

    public void GameOver()
    {
        GameState = EGameState.GameEnded;
        Managers.UI.Invoke("FadeInLoseScreen", _timeUntilLevelEndedScreen);
    }

    public void RestartLevel()
    {
        if (GameState == EGameState.Tutorial)
        {
            return;
        }

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
        if (GameState == EGameState.Tutorial)
        {
            return;
        }

        GameState = EGameState.GameRunning;
        Managers.Cubes.SelectedCube.SelectedSprite.SetActive(false);
        GameStarted?.Invoke();
        // ChangeToGameCamera();
    }

    private void ChangeToGameCamera()
    {
        Camera.main.GetComponent<FreeCamera>().enabled = false;
        Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
}