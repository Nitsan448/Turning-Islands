using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : CubeFace
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			ball.ChangeVelocity(GetVelocityFromDirection());
			Debug.Log(ball.GetComponent<Rigidbody2D>().velocity);
		}
	}
}
