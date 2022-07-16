using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public Portal ConnectedPortal;
	[SerializeField] private float portalDisableTime = 1;
	[SerializeField] private float _timeInsidePortal;

	private void Awake()
	{
		if(ConnectedPortal != null)
		{
			ConnectedPortal.ConnectedPortal = this;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			Vector3 newPosition = ConnectedPortal.transform.position;
			StartCoroutine(SwitchPortals(ball));
		}
	}

	private IEnumerator SwitchPortals(Ball ball)
	{
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		Vector3 newPosition = ConnectedPortal.transform.position;
		yield return new WaitForSeconds(_timeInsidePortal);
		ConnectedPortal.GetComponent<BoxCollider2D>().enabled = false;
		ball.transform.position = new Vector3(newPosition.x, newPosition.y + 0.05f, newPosition.z);
		ball.ChangeVelocity(ConnectedPortal.GetComponent<CubeFace>().GetVelocityFromDirection());
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(portalDisableTime);
		ConnectedPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
