using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeFace : MonoBehaviour
{
	[SerializeField] private float _velocityWhenLeavingFace = 10;
	public eDirection Direction;

	private AudioSource _audioSource;

	private void Awake()
	{
		if(GetComponent<BouncySurface>() != null)
		{
			_audioSource = GetComponent<AudioSource>();
		}
		else
		{
			Destroy(GetComponent<AudioSource>());
			_audioSource = GetComponentInChildren<AudioSource>();
		}
	}

	public Vector2 GetVelocity()
	{
		switch (Direction)
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
		Direction = DirectionExtensions.GetNewDirection(Direction, DirectionExtensions.GetIntByDirection(direction));
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			OnCollisionOrTrigger(ball);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Ball ball = collision.GetComponent<Ball>();
		if(ball != null)
		{
			OnCollisionOrTrigger(ball);
		}
	}

	protected virtual void OnCollisionOrTrigger(Ball ball)
	{
		Managers.Game.ResetTimeUntilGameOver();
		if (GetComponentInChildren<AudioSource>() != null)
		{
			GetComponentInChildren<AudioSource>().Play();
		}
	}
}
