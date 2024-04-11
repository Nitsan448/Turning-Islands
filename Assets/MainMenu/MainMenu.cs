using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private GameObject _levelSelectPanel;

    [SerializeField] private Button _playButton;

    [SerializeField] private Button _levelSelectButton;

    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _levelSelectBackToMainPanelButton;

    private void Awake()
    {
        _exitButton.onClick.AddListener(
            Application.Quit
        );

        _playButton.onClick.AddListener(
            delegate { SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel", 1)); }
        );

        _levelSelectButton.onClick.AddListener(
            delegate
            {
                _levelSelectPanel.SetActive(true);
                _mainPanel.SetActive(false);
            }
        );

        _levelSelectBackToMainPanelButton.onClick.AddListener(
            delegate
            {
                _mainPanel.SetActive(true);
                _levelSelectPanel.SetActive(false);
            }
        );
    }
}