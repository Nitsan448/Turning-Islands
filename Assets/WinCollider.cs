using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
	[SerializeField] private float _timeUntilWin = 1;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			StartCoroutine(CountToLevelWon());
		}
	}

	private IEnumerator CountToLevelWon()
	{
		Managers.GameManager.ChangeEffectorsState(false);
		yield return new WaitForSeconds(_timeUntilWin);
		Managers.GameManager.LevelWon();
	}
}
