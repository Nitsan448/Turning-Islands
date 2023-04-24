using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CubeFace : MonoBehaviour
{
    public eDirection Direction;
    protected abstract string SoundName { get; set; }
    private float timeUntilNextCollisionPossible = 0;
    protected abstract void OnCollisionOrTrigger(Ball ball);

    public Vector2 GetVelocity()
    {
        switch (Direction)
        {
            case eDirection.Top:
                return new Vector2(0, 1);
            case eDirection.Bottom:
                return new Vector2(0, -1);
            case eDirection.Right:
                return new Vector2(1, 0);
            case eDirection.Left:
                return new Vector2(-1, 0);
        }
        return Vector2.zero;
    }

    private void Update()
    {
        if (timeUntilNextCollisionPossible > 0)
        {
            timeUntilNextCollisionPossible -= Time.deltaTime;
        }
    }

    public void UpdateDirection(eDirection direction)
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
        if (ball != null && timeUntilNextCollisionPossible <= 0)
        {
            ball.StopCoroutine(ball.ArcMovementCoroutine);
            timeUntilNextCollisionPossible = 0.25f;
            ball.ResetTimeUntilGameOver();
            Managers.Audio.PlaySound(SoundName);
            if (GetComponentInChildren<AudioSource>() != null)
            {
                GetComponentInChildren<AudioSource>().Play();
            }
            OnCollisionOrTrigger(ball);
        }
    }
}
