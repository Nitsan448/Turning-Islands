using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup LoseScreen;
    [SerializeField] private CanvasGroup Controls;

    public void FadeInWinScreen()
	{
        GetComponent<Fader>().FadeInCanvasGroup(WinScreen);
	}

    public void FadeInLoseScreen()
    {
        GetComponent<Fader>().FadeInCanvasGroup(LoseScreen);
    }

    public void FadeInControls()
    {
        GetComponent<Fader>().FadeInCanvasGroup(Controls);
    }

    public void FadeOutControls()
    {
        GetComponent<Fader>().FadeOutCanvasGroup(Controls);
    }

    void Awake()
    {
        Managers.GameManager.UIManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
