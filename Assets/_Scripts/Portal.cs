using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : MonoBehaviour
{
	public Portal ConnectedPortal;
	[SerializeField] private float portalDisableTime = 1;
	[SerializeField] private float _timeInsidePortal;
	[SerializeField] private bool _tube;

	[SerializeField] private bool _open = true;
	private GameObject _portalSprite;

	private void Awake()
	{
		if (ConnectedPortal != null)
		{
			ConnectedPortal.ConnectedPortal = this;
		}
		if (!_tube)
		{
			_portalSprite = GetComponentInChildren<Light2D>().gameObject;
			_portalSprite.GetComponentInChildren<Light2D>().enabled = _open;
		}
		else
		{
			_open = true;
		}
	}

	public void ChangeOpenState()
	{
		_open = !_open;
		_portalSprite.GetComponentInChildren<Light2D>().enabled = _open;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null && _open)
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
		ball.ChangeVelocity(ConnectedPortal.GetComponent<CubeFace>().GetVelocity());
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(portalDisableTime);
		ConnectedPortal.GetComponent<BoxCollider2D>().enabled = true;
	}
}
