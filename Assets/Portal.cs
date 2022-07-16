using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : CubeFace
{
	[SerializeField] private Portal _connectedPortal;
	[SerializeField] private float portalDisableTime = 1;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			Vector3 newPosition = _connectedPortal.transform.position;
			ball.transform.position = new Vector3(newPosition.x, newPosition.y + 0.05f, newPosition.z);
			StartCoroutine(DisableConnectedPortal());
			ball.ChangeVelocity(_connectedPortal.GetVelocityFromDirection());
			Debug.Log(ball.gameObject.GetComponent<Rigidbody2D>().velocity);
		}
	}

	private IEnumerator DisableConnectedPortal()
	{
		_connectedPortal.GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(portalDisableTime);
		_connectedPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
