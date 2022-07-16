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
		
		while(currentTime < _timeToGameOver)
		{
			Managers.GameManager.Ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity().normalized * 0.45f);
			currentTime += Time.deltaTime;
			yield return null;
		}
		Managers.GameManager.EndGame();
	}
}
