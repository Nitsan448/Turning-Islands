using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameManager
{
    public CanvasGroupsLocator CanvasGroupLocator { get; set; }
    private Fader _fader;
    public void Startup()
    {
        _fader = GetComponent<Fader>();
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

    public void PlaySound()
	{
        GetComponent<AudioSource>().Play();
	}
}
