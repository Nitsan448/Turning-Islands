using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup WinScreen;
    [SerializeField] private CanvasGroup LoseScreen;

    public void FadeInWinScreen()
	{
        GetComponent<Fader>().FadeInCanvasGroup(WinScreen);
	}

    public void FadeInLoseScreen()
    {
        GetComponent<Fader>().FadeInCanvasGroup(LoseScreen);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
