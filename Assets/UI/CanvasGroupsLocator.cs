using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupsLocator : MonoBehaviour
{
    public CanvasGroup WinScreen;
    public CanvasGroup LoseScreen;
    public CanvasGroup ControlsScreen;

	private void Awake()
	{
		Managers.UI.CanvasGroupLocator = this;
	}
}
