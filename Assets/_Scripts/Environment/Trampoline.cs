using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : CubeFace
{
    protected override string SoundName { get; set; } = "No sound";
    public eDirection TrampolineDirection = eDirection.Right;

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        Vector2 cubeFaceVelocity = GetComponent<CubeFace>().GetVelocity();
        ball.ChangeVelocity(Vector2.zero);
        Cube neighborCube = FindNeighborCube();
        Vector2 targetPosition = neighborCube
            .GetCubeFaceObjectByDirection(Direction)
            .transform.position;

        targetPosition = new Vector2(
            neighborCube
                .GetCubeFaceObjectByDirection(Direction)
                .GetComponent<BoxCollider2D>()
                .offset.y + targetPosition.x,
            neighborCube
                .GetCubeFaceObjectByDirection(Direction)
                .GetComponent<BoxCollider2D>()
                .offset.y + targetPosition.y
        );

        ball.GetComponent<Animator>().Play("Squish");
        StartCoroutine(ball.MoveTowardInArc(8, targetPosition, Direction));
    }

    private Cube FindNeighborCube()
    {
        // Problem with win flag making win cube not a neighbor
        Cube neighborCube = null;
        if (
            (TrampolineDirection == eDirection.Right && Direction == eDirection.Top)
            || (TrampolineDirection == eDirection.Left && Direction == eDirection.Bottom)
        )
        {
            neighborCube = transform.parent.GetComponent<Cube>().RightCube;
        }
        else if (
            (TrampolineDirection == eDirection.Left && Direction == eDirection.Top)
            || (TrampolineDirection == eDirection.Right && Direction == eDirection.Bottom)
        )
        {
            neighborCube = transform.parent.GetComponent<Cube>().LeftCube;
        }
        else if (
            (TrampolineDirection == eDirection.Left && Direction == eDirection.Left)
            || (TrampolineDirection == eDirection.Right && Direction == eDirection.Right)
        )
        {
            neighborCube = transform.parent.GetComponent<Cube>().BottomCube;
        }
        else if (
            (TrampolineDirection == eDirection.Right && Direction == eDirection.Left)
            || (TrampolineDirection == eDirection.Left && Direction == eDirection.Right)
        )
        {
            neighborCube = transform.parent.GetComponent<Cube>().TopCube;
        }
        Debug.Log(neighborCube);
        return neighborCube;
    }
}
