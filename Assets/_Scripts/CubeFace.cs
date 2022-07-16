using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeFace : MonoBehaviour
{
	[SerializeField] private float _velocityWhenLeavingFace;
	[SerializeField] private eDirection _direction;

	protected Vector2 GetVelocityFromDirection()
	{
		switch (_direction)
		{
			case eDirection.Up:
				return new Vector2(0, _velocityWhenLeavingFace);
			case eDirection.Down:
				return new Vector2(0, -_velocityWhenLeavingFace);
			case eDirection.Right:
				return new Vector2(_velocityWhenLeavingFace, 0);
			case eDirection.Left:
				return new Vector2(-_velocityWhenLeavingFace, 0);
		}
		return Vector2.zero;
	}

	public void UpdateDirection(eDirection direction)
	{
		_direction = Helpers.GetNewDirection(_direction, direction);
	}
}
