using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : CubeFace
{
	protected override void OnCollisionOrTrigger(Ball ball)
	{
		base.OnCollisionOrTrigger(ball);
		ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
		ball.GetComponent<Animator>().Play("Squish");
	}
}
