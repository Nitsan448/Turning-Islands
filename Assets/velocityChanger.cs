using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityChanger : MonoBehaviour
{
	[SerializeField] private Vector2 _newVelocity;
	[SerializeField] private bool disableOnEnter;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			ball.ChangeVelocity(_newVelocity);
			if (disableOnEnter)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
