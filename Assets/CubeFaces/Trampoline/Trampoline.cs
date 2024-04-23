using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : CubeFace
{
    protected override string SoundName { get; set; } = "Trampoline";
    public EDirection TrampolineDirection = EDirection.Right;
    private Animator _animator;
    private Cube _parentCube;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _parentCube = transform.parent.parent.GetComponent<Cube>();
    }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        Vector2 cubeFaceVelocity = GetComponent<CubeFace>().GetVelocity();
        ball.ChangeVelocity(Vector2.zero);
        _animator.SetTrigger("TrampolineHit");

        Vector2 targetPosition = GetTargetPosition();

        ball.Animator.Play("Squish");
        ball.ArcMovementCoroutine = ball.StartCoroutine(
            ball.MoveTowardInArc(8, targetPosition, Direction)
        );
    }

    private Vector2 GetTargetPosition()
    {
        EDirection movementDirection = GetMovementDirection();

        Vector2 targetPosition = GetTargetPositionWithoutOffset(movementDirection);

        Cube neighborCube = CubeExtensions.FindAdjacentCubeByDirection(
            _parentCube,
            movementDirection
        );
        if (neighborCube != null)
        {
            AddTargetFaceColliderOffset(ref targetPosition, neighborCube);
        }

        return targetPosition;
    }

    private Vector2 GetTargetPositionWithoutOffset(EDirection movementDirection)
    {
        Vector2 targetPosition = transform.position;
        switch (movementDirection)
        {
            case EDirection.Top:
                targetPosition.y += Managers.Cubes.DistanceBetweenCubes.y;
                break;
            case EDirection.Right:
                targetPosition.x += Managers.Cubes.DistanceBetweenCubes.x;
                break;
            case EDirection.Bottom:
                targetPosition.y -= Managers.Cubes.DistanceBetweenCubes.y;
                break;
            case EDirection.Left:
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

        int sign = targetFaceColliderOffset.y > 0 ? 1 : -1;
        switch (Direction)
        {
            case EDirection.Top:
                targetPosition.y += targetFaceColliderOffset.y * sign + 0.2f;
                break;
            case EDirection.Right:
                targetPosition.x += targetFaceColliderOffset.y * sign + 0.2f;
                break;
            case EDirection.Bottom:
                targetPosition.y -= targetFaceColliderOffset.y * sign + 0.2f;
                break;
            case EDirection.Left:
                targetPosition.x -= targetFaceColliderOffset.y * sign + 0.2f;
                break;
        }
    }

    private EDirection GetMovementDirection()
    {
        Dictionary<(EDirection, EDirection), EDirection> directionMap = new Dictionary<
            (EDirection, EDirection),
            EDirection
        >
        {
            { (EDirection.Right, EDirection.Top), EDirection.Right },
            { (EDirection.Left, EDirection.Bottom), EDirection.Right },
            { (EDirection.Left, EDirection.Top), EDirection.Left },
            { (EDirection.Right, EDirection.Bottom), EDirection.Left },
            { (EDirection.Left, EDirection.Left), EDirection.Bottom },
            { (EDirection.Right, EDirection.Right), EDirection.Bottom },
            { (EDirection.Right, EDirection.Left), EDirection.Top },
            { (EDirection.Left, EDirection.Right), EDirection.Top },
        };

        EDirection movementDirection = directionMap[(TrampolineDirection, Direction)];
        return movementDirection;
    }
}