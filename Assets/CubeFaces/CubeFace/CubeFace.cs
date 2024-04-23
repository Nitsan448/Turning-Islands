using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CubeFace : MonoBehaviour
{
    public EDirection Direction;
    protected abstract string SoundName { get; set; }
    private float timeUntilNextCollisionPossible = 0;
    protected abstract void OnCollisionOrTrigger(Ball ball);
    protected EDirection _startingDirection;

    private void Start()
    {
        _startingDirection = Direction;
    }

    public Vector2 GetVelocity()
    {
        switch (Direction)
        {
            case EDirection.Top:
                return new Vector2(0, 1);
            case EDirection.Bottom:
                return new Vector2(0, -1);
            case EDirection.Right:
                return new Vector2(1, 0);
            case EDirection.Left:
                return new Vector2(-1, 0);
        }

        return Vector2.zero;
    }

    //TODO: remove the update method
    private void Update()
    {
        if (timeUntilNextCollisionPossible > 0)
        {
            timeUntilNextCollisionPossible -= Time.deltaTime;
        }
    }

    public void UpdateDirection(EDirection direction)
    {
        Direction = DirectionExtensions.GetNewDirection(
            Direction,
            DirectionExtensions.GetIntByDirection(direction)
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null && timeUntilNextCollisionPossible <= 0)
        {
            if (ball.ArcMovementCoroutine != null)
            {
                ball.StopCoroutine(ball.ArcMovementCoroutine);
            }

            timeUntilNextCollisionPossible = 0.25f;
            ball.ResetTimeUntilGameOver();
            Managers.Audio.PlaySound(SoundName);

            OnCollisionOrTrigger(ball);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        if (ball != null && timeUntilNextCollisionPossible <= 0)
        {
            if (ball.ArcMovementCoroutine != null)
            {
                ball.StopCoroutine(ball.ArcMovementCoroutine);
            }

            timeUntilNextCollisionPossible = 0.25f;
            ball.ResetTimeUntilGameOver();
            Managers.Audio.PlaySound(SoundName);

            OnCollisionOrTrigger(ball);
        }
    }
}