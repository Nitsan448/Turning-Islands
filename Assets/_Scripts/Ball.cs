using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 StartingVelocity = new Vector2(0, -1);

    [SerializeField] private float _speed = 4;

    private float _startingTimeUntilGameOver = 3;
    private float _timeUntilGameOver = 3;
    public Coroutine ArcMovementCoroutine;
    public bool BallInFlag = false;

    private Vector2 _velocity;
    private Animator _animator;
    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

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

    private void Update()
    {
        if (Managers.Game.GameState == EGameState.GameRunning && !BallInFlag)
        {
            _timeUntilGameOver -= Time.deltaTime;
            if (_timeUntilGameOver <= 0)
            {
                Managers.Game.GameOver();
            }
        }
    }

    public void ResetTimeUntilGameOver()
    {
        _timeUntilGameOver = _startingTimeUntilGameOver;
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
            float progress = currentTime / duration;
            if (direction == eDirection.Top || direction == eDirection.Bottom)
            {
                float newXPosition = Mathf.Lerp(startPosition.x, targetPosition.x, progress);
                float distance = Mathf.Abs(targetPosition.y - startPosition.y);
                bool isOffSetPositive = direction != eDirection.Bottom;
                float yOffset = calculateOffset(progress, duration, distance, isOffSetPositive);
                transform.position = new Vector2(newXPosition, startPosition.y + yOffset);
            }
            else
            {
                float newYPosition = Mathf.Lerp(startPosition.y, targetPosition.y, progress);
                float distance = Mathf.Abs(targetPosition.x - startPosition.x);
                bool isOffSetPositive = direction != eDirection.Left;
                float xOffset = calculateOffset(progress, duration, distance, isOffSetPositive);
                transform.position = new Vector2(startPosition.x + xOffset, newYPosition);
            }

            currentTime += Time.deltaTime * _speed;
            yield return null;
        }

        transform.position = targetPosition;
        ArcMovementCoroutine = null;
    }

    private float calculateOffset(float progress, float duration, float distance, bool isPositive)
    {
        float offset = 2 * Mathf.Sin(progress * Mathf.PI);
        offset -= progress * distance;
        if (!isPositive)
        {
            offset *= -1;
        }

        return offset;
    }
}