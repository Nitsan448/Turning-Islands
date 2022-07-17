using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
	[SerializeField] private float _timeToGameOver = 1;
	private float currentTime = 0;
	private float timeInterval = 0.3f;
	private int velocitySign = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			//GetComponentInChildren<PointEffector2D>().enabled = false;
			ball.ChangeVelocity(Vector2.zero);
			StartCoroutine(CountToGameOver());
		}
	}

	private void FixedUpdate()
	{
		if (Managers.GameManager.GameEnded)
		{
			if (currentTime > timeInterval)
			{
				Managers.GameManager.Ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity().normalized * 0.4f * velocitySign);
				velocitySign *= -1;
				currentTime = 0;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
		}
	}

	private IEnumerator CountToGameOver()
	{
		Managers.GameManager.GameEnded = true;
		Managers.GameManager.ChangeEffectorsState(false);
		yield return new WaitForSeconds(_timeToGameOver);
		Managers.GameManager.GameOver();
	}
}
