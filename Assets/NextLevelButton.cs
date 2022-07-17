using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnEnable()
	{
		Debug.Log(GetComponent<Button>());
		gameObject.GetComponent<Button>().onClick.AddListener(delegate { Managers.GameManager.NextLevel(); });
	}
}
