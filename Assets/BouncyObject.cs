using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
	[SerializeField] private float _bounceVelocity;
	[SerializeField] private eDirection _direction;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			ball.ChangeVelocity(GetVelocityFromDirection());
		}
	}

	private Vector2 GetVelocityFromDirection()
	{
		switch (_direction)
		{
			case eDirection.Up:
				return new Vector2(0, _bounceVelocity);
			case eDirection.Down:
				return new Vector2(0, -_bounceVelocity);
			case eDirection.Right:
				return new Vector2(_bounceVelocity, 0);
			case eDirection.Left:
				return new Vector2(-_bounceVelocity, 0);
		}
		return Vector2.zero;
	}

	public void UpdateDirection(eDirection direction)
	{
		_direction = Helpers.GetNewDirection(_direction, direction);
		Debug.Log(_direction);
	}
}
