using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsListenersAdder : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _restartButtonBottomBorder;
    [SerializeField] private Button _exitControlsMenuButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _controlsMenuButton;
    [SerializeField] private Button _playButton;

    void Awake()
    {
        _restartButton.onClick.AddListener(delegate { Managers.Game.RestartLevel(); });
        _restartButtonBottomBorder.onClick.AddListener(delegate { Managers.Game.RestartLevel(); });
        _nextLevelButton.onClick.AddListener(delegate { Managers.Game.GoToNextLevel(); });
        _exitControlsMenuButton.onClick.AddListener(delegate { Managers.UI.FadeOutControls(); });
        _exitButton.onClick.AddListener(delegate { Application.Quit(); });
        _playButton.onClick.AddListener(delegate { Managers.Game.StartGame(); });
        _controlsMenuButton.onClick.AddListener(delegate { Managers.UI.FadeInControls(); });
    }
}
