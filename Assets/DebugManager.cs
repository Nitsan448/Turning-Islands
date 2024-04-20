using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    private Dictionary<string, Action> _commands;
    private string _currentInput = "";

    private void Start()
    {
        _commands = new() { { "next", Managers.Game.GoToNextLevel }, { "clear", PlayerPrefs.DeleteAll } };
        for (int levelIndex = 0; levelIndex < SceneManager.sceneCountInBuildSettings; levelIndex++)
        {
            //TODO: go over all scenes and add ones who's name starts with level
            int levelNumber = levelIndex + 1;
            _commands.Add("level" + (levelNumber), delegate { SceneManager.LoadScene("Level" + (levelNumber)); });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendInput();
            _currentInput = "";
        }

        _currentInput += Input.inputString;
    }

    private void SendInput()
    {
        if (_commands.Keys.Contains(_currentInput))
        {
            _commands[_currentInput]();
        }
    }
}