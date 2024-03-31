using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGameManager
{
    public CanvasGroupsLocator CanvasGroupLocator { get; set; }
    private Fader _fader;

    public void Startup()
    {
        _fader = GetComponent<Fader>();
        foreach (Button button in FindObjectsOfType<Button>(true))
        {
            button.onClick.AddListener(delegate { Managers.Audio.PlaySound("UIClick"); });
        }
    }

    public void FadeInWinScreen()
    {
        _fader.FadeInCanvasGroup(CanvasGroupLocator.WinScreen);
    }

    public void FadeInLoseScreen()
    {
        _fader.FadeInCanvasGroup(CanvasGroupLocator.LoseScreen);
    }

    public void FadeInControls()
    {
        _fader.FadeInCanvasGroup(CanvasGroupLocator.ControlsScreen);
    }

    public void FadeOutControls()
    {
        _fader.FadeOutCanvasGroup(CanvasGroupLocator.ControlsScreen);
    }

    public void FadeOutLoseScreen()
    {
        _fader.FadeOutCanvasGroup(CanvasGroupLocator.LoseScreen);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}