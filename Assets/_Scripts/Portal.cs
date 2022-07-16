using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : CubeFace
{
	[SerializeField] private Portal _connectedPortal;
	[SerializeField] private float portalDisableTime = 1;
	[SerializeField] private float _timeInsidePortal;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			Vector3 newPosition = _connectedPortal.transform.position;
			StartCoroutine(SwitchPortals(ball));
		}
	}

	private IEnumerator SwitchPortals(Ball ball)
	{
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		Vector3 newPosition = _connectedPortal.transform.position;
		yield return new WaitForSeconds(_timeInsidePortal);
		_connectedPortal.GetComponent<BoxCollider2D>().enabled = false;
		ball.transform.position = new Vector3(newPosition.x, newPosition.y + 0.05f, newPosition.z);
		ball.ChangeVelocity(_connectedPortal.GetVelocityFromDirection());
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(portalDisableTime);
		_connectedPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
