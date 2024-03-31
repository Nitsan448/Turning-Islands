using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string _tutorialName;
    [SerializeField] private Button _finishTutorialButton;
    [SerializeField] protected Ball _ball;
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
            ResetTutorialState();
        }

        _timeSinceTutorialStarted += Time.deltaTime;
    }

    protected virtual void ResetTutorialState()
    {
        _ball.ResetToStartingState();
        _ball.StartMoving();
        _timeSinceTutorialStarted = 0;
    }

    public void OnTutorialFinished()
    {
        Managers.Game.SetStateToEditing();
        PlayerPrefs.SetInt(_tutorialName, 1);
        gameObject.SetActive(false);
    }

    [Button]
    private void SetSortingLayersToTutorial()
    {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.sortingLayerName = "Tutorial";
        }
    }
}