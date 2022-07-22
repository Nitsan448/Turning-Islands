using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : CubeFace
{
	public Portal ConnectedPortal { get; set; }
	public bool IsOpen { get; set; } = true;
	public int PortalIndex { get; set; } = -1;
	public bool IsTube { get; set; }
	public float TimeInsidePortal { get; set; } = 0;

	private float portalDisableTime = 1;
	private GameObject _portalSprite;

	private void Awake()
	{
		if (ConnectedPortal != null)
		{
			ConnectedPortal.ConnectedPortal = this;
		}
		if (!IsTube)
		{
			_portalSprite = GetComponentInChildren<Light2D>().gameObject;
			_portalSprite.GetComponentInChildren<Light2D>().enabled = IsOpen;
		}
		else
		{
			IsOpen = true;
		}
	}

	public void ChangeOpenState()
	{
		IsOpen = !IsOpen;
		GetComponentInChildren<Light2D>().enabled = IsOpen;
	}

	protected override void OnCollisionOrTrigger(Ball ball)
	{
		base.OnCollisionOrTrigger(ball);
		if (IsOpen)
		{
			StartCoroutine(SwitchPortals(ball));
		}
		else
		{
			ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
			ball.GetComponent<Animator>().Play("Squish");
		}
	}

	private IEnumerator SwitchPortals(Ball ball)
	{
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(TimeInsidePortal);

		ConnectedPortal.GetComponent<BoxCollider2D>().enabled = false;
		Vector3 newPosition = ConnectedPortal.transform.position;
		ball.transform.position = new Vector3(newPosition.x, newPosition.y + 0.05f, newPosition.z);
		ball.ChangeVelocity(ConnectedPortal.GetComponent<CubeFace>().GetVelocity());
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(portalDisableTime);

		ConnectedPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
