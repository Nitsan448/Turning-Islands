using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : CubeFace
{
    [SerializeField] private Portal connectedPortal;
	protected override string SoundName { get; set; } = "No Sound";
	protected override void OnCollisionOrTrigger(Ball ball)
	{
		connectedPortal.ChangeOpenState();
		connectedPortal.ConnectedPortal.ChangeOpenState();
		ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
		ball.GetComponent<Animator>().Play("Squish");
	}
}
