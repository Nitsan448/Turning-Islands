using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private Vector2 _startingVelocity = new Vector2(0, -1);
	[SerializeField] private float _speed = 4;

	private Vector2 _velocity;

	private void OnEnable()
	{
		Managers.Game.GameStarted += StartMoving;
	}

	private void OnDisable()
	{
		Managers.Game.GameStarted -= StartMoving;
	}

	public void StartMoving()
	{
		ChangeVelocity(_startingVelocity);
	}

	private void FixedUpdate()
	{
		float xPosition = transform.position.x + _velocity.x * Time.deltaTime * _speed;
		float yPosition = transform.position.y + _velocity.y * Time.deltaTime * _speed;
		transform.position = new Vector2(xPosition, yPosition);
	}

	public void ChangeVelocity(Vector2 velocity)
	{
		_velocity = velocity;
	}


	public IEnumerator MoveTowardInArc(float duration, Vector2 targetPosition, eDirection direction)
	{
		Vector2 startPosition = transform.position;
		float currentTime = 0;

		while (currentTime < duration)
		{
			if(direction == eDirection.Top || direction == eDirection.Bottom)
			{
				float newXPosition = Mathf.Lerp(startPosition.x, targetPosition.x, currentTime / duration);
				float heightOffset = 2 * Mathf.Sin(currentTime * Mathf.PI / duration);
				transform.position = new Vector2(newXPosition, startPosition.y + heightOffset);
			}
			else
			{
				float newYPosition = Mathf.Lerp(startPosition.y, targetPosition.y, currentTime / duration);
				float heightOffset = 2 * Mathf.Sin(currentTime * Mathf.PI / duration);
				transform.position = new Vector2(startPosition.y + heightOffset, newYPosition);
			}
			currentTime += Time.deltaTime * _speed;
			yield return null;
		}
		transform.position = targetPosition;
	}
}