using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
	private bool ballHitMagnet;
	private float currentTime = 0;
	private float timeInterval = 0.3f;
	private int velocitySign = 1;
	private Ball _ball;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_ball = collision.gameObject.GetComponent<Ball>();
		if (_ball != null)
		{
			_ball.ChangeVelocity(Vector2.zero);
			Managers.Game.GameOver();
		}
	}

	private void FixedUpdate()
	{
		if (ballHitMagnet)
		{
			//TODO: Do this using normal animation!
			BallInMagnetAnimation();
		}
	}

	private void BallInMagnetAnimation()
	{
		if (currentTime > timeInterval)
		{
			_ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity().normalized * 0.4f * velocitySign);
			velocitySign *= -1;
			currentTime = 0;
		}
		else
		{
			currentTime += Time.deltaTime;
		}
	}
}
