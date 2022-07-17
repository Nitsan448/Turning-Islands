using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	private void OnEnable()
	{
        GetComponent<Button>().onClick.AddListener(delegate { Managers.GameManager.Restart(); });
    }
}
