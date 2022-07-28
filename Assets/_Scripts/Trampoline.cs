using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : CubeFace
{
    protected override string SoundName { get; set; } = "No sound";
	[SerializeField] private eDirection _trampolineDirection = eDirection.Right;

	protected override void OnCollisionOrTrigger(Ball ball)
	{
		Vector2 cubeFaceVelocity = GetComponent<CubeFace>().GetVelocity();
		ball.ChangeVelocity(Vector2.zero);
		Cube neighborCube = FindNeighborCube();
		StartCoroutine(ball.MoveTowardInArc(8, neighborCube.GetCubeFaceObjectByDirection(Direction).transform.position, Direction));
		ball.GetComponent<Animator>().Play("Squish");
	}

	private Cube FindNeighborCube()
	{
		Cube neighborCube = null;
		if (_trampolineDirection == eDirection.Right)
		{
			neighborCube = transform.parent.GetComponent<Cube>().RightCube;
		}
		else
		{
			neighborCube = transform.parent.GetComponent<Cube>().LeftCube;
		}
		return neighborCube;
	}
}
