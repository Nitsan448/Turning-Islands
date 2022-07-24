using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CubeFace : MonoBehaviour
{
	[SerializeField] private float _velocityWhenLeavingFace = 10;
	public eDirection Direction;

	protected abstract string SoundName { get; set; }
	protected abstract void OnCollisionOrTrigger(Ball ball);

	public Vector2 GetVelocity()
	{
		switch (Direction)
		{
			case eDirection.Top:
				return new Vector2(0, _velocityWhenLeavingFace);
			case eDirection.Bottom:
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
		Direction = DirectionExtensions.GetNewDirection(Direction, DirectionExtensions.GetIntByDirection(direction));
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			Managers.Game.ResetTimeUntilGameOver();
			Managers.Audio.PlaySound(SoundName);
			if (GetComponentInChildren<AudioSource>() != null)
			{
				GetComponentInChildren<AudioSource>().Play();
			}
			OnCollisionOrTrigger(ball);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.GetComponent<Ball>();
		if(ball != null)
		{
			Managers.Game.ResetTimeUntilGameOver();
			Managers.Audio.PlaySound(SoundName);
			if (GetComponentInChildren<AudioSource>() != null)
			{
				GetComponentInChildren<AudioSource>().Play();
			}
			OnCollisionOrTrigger(ball);
		}
	}

}
