using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _timeToReachTargetPosition;

    [SerializeField] private bool _isVertical;
    private Vector2 _targetPosition;
    private Vector2 _startingPosition;
    [SerializeField] private bool _moveDownOrLeft;
    private float _timeSinceLastDirectionChange = 0;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        int distanceSign = _moveDownOrLeft ? 1 : -1;
        _targetPosition = _isVertical
            ? new Vector2(_startingPosition.x, _startingPosition.y - _distance * distanceSign)
            : new Vector2(_startingPosition.x - _distance * distanceSign, _startingPosition.y);
        transform.position = Vector2.Lerp(_startingPosition, _targetPosition, _timeSinceLastDirectionChange / _timeToReachTargetPosition);
        _timeSinceLastDirectionChange += Time.deltaTime;

        if (_timeSinceLastDirectionChange >= _timeToReachTargetPosition)
        {
            transform.position = _targetPosition;
            _startingPosition = transform.position;
            _moveDownOrLeft = !_moveDownOrLeft;
            _timeSinceLastDirectionChange = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        int distanceSign = _moveDownOrLeft ? 1 : -1;
        Vector2 targetPosition = _isVertical
            ? new Vector2(transform.position.x, transform.position.y - _distance * distanceSign)
            : new Vector2(transform.position.x - _distance * distanceSign, transform.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPosition);
    }
}