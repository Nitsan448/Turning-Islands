using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
	[SerializeField] private float _timeToGameOver;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			ball.ChangeVelocity(Vector2.zero);
			StartCoroutine(CountToGameOver());
		}
	}

	private IEnumerator CountToGameOver()
	{
		float currentTime = 0;
		Managers.GameManager.ChangeEffectorsState(false);
		int velocitySign = 1;
		while (currentTime < _timeToGameOver)
		{
			Managers.GameManager.Ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity().normalized * 0.4f * velocitySign);
			velocitySign *= -1;
			currentTime += 0.3f;
			yield return new WaitForSeconds(0.3f);
		}
		Managers.GameManager.EndGame();
	}
}
