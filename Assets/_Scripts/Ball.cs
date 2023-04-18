using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 StartingVelocity = new Vector2(0, -1);

    [SerializeField]
    private float _speed = 4;

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
        _velocity = StartingVelocity;
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

    public IEnumerator MoveTowardInArc(
        float duration,
        Vector2 startingTargetPosition,
        eDirection direction
    )
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startingTargetPosition;
        float currentTime = 0;
        while (currentTime < duration)
        {
            targetPosition = startingTargetPosition;
            float offset = 2 * Mathf.Sin(currentTime * Mathf.PI / duration);

            if (direction == eDirection.Top || direction == eDirection.Bottom)
            {
                float newXPosition = Mathf.Lerp(
                    startPosition.x,
                    targetPosition.x,
                    currentTime / duration
                );
                offset -= currentTime / duration * Mathf.Abs(targetPosition.y - startPosition.y);
                if (direction == eDirection.Bottom)
                {
                    offset *= -1;
                }
                transform.position = new Vector2(newXPosition, startPosition.y + offset);
            }
            else
            {
                float newYPosition = Mathf.Lerp(
                    startPosition.y,
                    targetPosition.y,
                    currentTime / duration
                );
                offset -= currentTime / duration * Mathf.Abs(targetPosition.x - startPosition.x);
                if (direction == eDirection.Right)
                {
                    offset *= -1;
                }
                transform.position = new Vector2(startPosition.x - offset, newYPosition);
            }
            currentTime += Time.deltaTime * _speed;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
