using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : CubeFace
{
    protected override string SoundName { get; set; } = "No sound";
    public eDirection TrampolineDirection = eDirection.Right;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        Vector2 cubeFaceVelocity = GetComponent<CubeFace>().GetVelocity();
        ball.ChangeVelocity(Vector2.zero);
        _animator.SetTrigger("TrampolineHit");

        Vector2 targetPosition = getTargetPosition();

        ball.Animator.Play("Squish");
        ball.ArcMovementCoroutine = ball.StartCoroutine(
            ball.MoveTowardInArc(8, targetPosition, Direction)
        );
    }

    private Vector2 getTargetPosition()
    {
        eDirection movementDirection = GetMovementDirection();

        Vector2 targetPosition = GetTargetPositionWithoutOffset(movementDirection);

        Debug.Log(targetPosition);
        Cube neighborCube = CubeExtensions.FindAdjacentCubeByDirection(
            transform.parent.GetComponent<Cube>(),
            movementDirection
        );
        if (neighborCube != null)
        {
            AddTargetFaceColliderOffset(ref targetPosition, neighborCube);
        }

        Debug.Log(targetPosition);
        return targetPosition;
    }

    private Vector2 GetTargetPositionWithoutOffset(eDirection movementDirection)
    {
        Vector2 targetPosition = transform.position;
        switch (movementDirection)
        {
            case eDirection.Top:
                targetPosition.y += Managers.Cubes.DistanceBetweenCubes.y;
                break;
            case eDirection.Right:
                targetPosition.x += Managers.Cubes.DistanceBetweenCubes.x;
                break;
            case eDirection.Bottom:
                targetPosition.y -= Managers.Cubes.DistanceBetweenCubes.y;
                break;
            case eDirection.Left:
                targetPosition.x -= Managers.Cubes.DistanceBetweenCubes.x;
                break;
        }

        return targetPosition;
    }

    private void AddTargetFaceColliderOffset(ref Vector2 targetPosition, Cube neighborCube)
    {
        Vector2 targetFaceColliderOffset = neighborCube
            .GetCubeFaceObjectByDirection(Direction)
            .GetComponent<BoxCollider2D>()
            .offset;

        Debug.Log(targetFaceColliderOffset.y);
        switch (Direction)
        {
            case eDirection.Top:
                targetPosition.y -= targetFaceColliderOffset.y - 0.2f;
                break;
            case eDirection.Right:
                targetPosition.x -= targetFaceColliderOffset.y + 0.2f;
                break;
            case eDirection.Bottom:
                targetPosition.y += targetFaceColliderOffset.y + 0.2f;
                break;
            case eDirection.Left:
                targetPosition.x += targetFaceColliderOffset.y - 0.2f;
                break;
        }
    }

    private eDirection GetMovementDirection()
    {
        Dictionary<(eDirection, eDirection), eDirection> directionMap = new Dictionary<
            (eDirection, eDirection),
            eDirection
        >
        {
            { (eDirection.Right, eDirection.Top), eDirection.Right },
            { (eDirection.Left, eDirection.Bottom), eDirection.Right },
            { (eDirection.Left, eDirection.Top), eDirection.Left },
            { (eDirection.Right, eDirection.Bottom), eDirection.Left },
            { (eDirection.Left, eDirection.Left), eDirection.Bottom },
            { (eDirection.Right, eDirection.Right), eDirection.Bottom },
            { (eDirection.Right, eDirection.Left), eDirection.Top },
            { (eDirection.Left, eDirection.Right), eDirection.Top },
        };

        eDirection movementDirection = directionMap[(TrampolineDirection, Direction)];
        return movementDirection;
    }
}