using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string _tutorialName;
    [SerializeField] private Button _finishTutorialButton;
    [SerializeField] private Ball _ball;
    [SerializeField] private float _tutorialLength;
    private float _timeSinceTutorialStarted = 0;

    void Start()
    {
        if (PlayerPrefs.GetInt(_tutorialName, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        _ball.StartMoving();
        Managers.Game.SetStateToTutorial(this);
        _finishTutorialButton.onClick.AddListener(OnTutorialFinished);
    }

    private void Update()
    {
        if (_timeSinceTutorialStarted > _tutorialLength)
        {
            _ball.ResetToStartingState();
            _ball.StartMoving();
            _timeSinceTutorialStarted = 0;
        }

        _timeSinceTutorialStarted += Time.deltaTime;
    }

    public void OnTutorialFinished()
    {
        Managers.Game.SetStateToEditing();
        PlayerPrefs.SetInt(_tutorialName, 1);
        gameObject.SetActive(false);
    }
}