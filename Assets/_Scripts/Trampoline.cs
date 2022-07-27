using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : CubeFace
{
    protected override string SoundName { get; set; } = "No sound";
	[SerializeField] private float _velocityOtherFactor = 1.5f;

	protected override void OnCollisionOrTrigger(Ball ball)
	{
		Vector2 cubeFaceVelocity = GetComponent<CubeFace>().GetVelocity();
		Vector2 trampolineVelocity;
		if (cubeFaceVelocity.x > 0)
		{
			trampolineVelocity = new Vector2(cubeFaceVelocity.x, _velocityOtherFactor * cubeFaceVelocity.x);
		}
		else
		{
			trampolineVelocity = new Vector2(_velocityOtherFactor * cubeFaceVelocity.y, cubeFaceVelocity.y);
		}
		ball.ChangeVelocity(trampolineVelocity);
		ball.GetComponent<Animator>().Play("Squish");
	}
}
