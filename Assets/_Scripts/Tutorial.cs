using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string _tutorialName;
    [SerializeField] private Button _finishTutorialButton;

    void Start()
    {
        if (PlayerPrefs.GetInt(_tutorialName, 0) == 1)
        {
            Destroy(gameObject);
            return;
        }

        Managers.Game.SetStateToTutorial(this);
        _finishTutorialButton.onClick.AddListener(OnTutorialFinished);
    }

    public void OnTutorialFinished()
    {
        Managers.Game.SetStateToEditing();
        PlayerPrefs.SetInt(_tutorialName, 1);
        gameObject.SetActive(false);
    }
}