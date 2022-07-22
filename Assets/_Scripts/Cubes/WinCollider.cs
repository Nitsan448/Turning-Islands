using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : CubeFace
{
	protected override void OnCollisionOrTrigger(Ball ball)
	{
		base.OnCollisionOrTrigger(ball);
		GetComponentInChildren<Animator>().Play("Sploosh");
		Managers.Game.LevelWon();
	}
}
