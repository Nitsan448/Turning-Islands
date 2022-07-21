using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    [SerializeField] private Portal connectedPortal;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if(ball != null)
		{
			connectedPortal.ChangeOpenState();
			connectedPortal.ConnectedPortal.ChangeOpenState();
			ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
			ball.GetComponent<Animator>().Play("Squish");
		}
	}
}
