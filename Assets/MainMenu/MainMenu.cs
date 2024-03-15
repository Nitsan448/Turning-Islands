using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;

    [SerializeField] private GameObject _controlsPanel;

    [SerializeField] private GameObject _levelSelectPanel;

    [SerializeField] private Button _playButton;

    [SerializeField] private Button _controlsButton;

    [SerializeField] private Button _levelSelectButton;

    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _controlsBackToMainPanelButton;
    [SerializeField] private Button _levelSelectBackToMainPanelButton;

    [SerializeField] private TextMeshProUGUI _panelTitle;

    private void Awake()
    {
        _exitButton.onClick.AddListener(
            Application.Quit
        );

        _playButton.onClick.AddListener(
            delegate { SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel", 1)); }
        );

        _controlsButton.onClick.AddListener(
            delegate
            {
                _controlsPanel.SetActive(true);
                _panelTitle.text = "Controls";
                _mainPanel.SetActive(false);
            }
        );

        _levelSelectButton.onClick.AddListener(
            delegate
            {
                _levelSelectPanel.SetActive(true);
                _panelTitle.text = "Level Select";
                _mainPanel.SetActive(false);
            }
        );

        _controlsBackToMainPanelButton.onClick.AddListener(
            delegate
            {
                _mainPanel.SetActive(true);
                _panelTitle.text = "Main Menu";
                _controlsPanel.SetActive(false);
                _levelSelectPanel.SetActive(false);
            }
        );
        _levelSelectBackToMainPanelButton.onClick.AddListener(
            delegate
            {
                _mainPanel.SetActive(true);
                _controlsPanel.SetActive(false);
                _panelTitle.text = "Main Menu";
                _levelSelectPanel.SetActive(false);
            }
        );
    }
}