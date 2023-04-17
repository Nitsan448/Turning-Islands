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
        Vector2 targetDirection = neighborCube
            .GetCubeFaceObjectByDirection(Direction)
            .transform.position;
        targetDirection = new Vector2(
            targetDirection.x,
            neighborCube
                .GetCubeFaceObjectByDirection(Direction)
                .GetComponent<BoxCollider2D>()
                .offset.y + targetDirection.y
        );
        StartCoroutine(ball.MoveTowardInArc(8, targetDirection, Direction));
        ball.GetComponent<Animator>().Play("Squish");
    }

    private Cube FindNeighborCube()
    {
        Cube neighborCube = null;
        if (TrampolineDirection == eDirection.Right)
        {
            neighborCube = transform.parent.GetComponent<Cube>().RightCube;
        }
        else
        {
            neighborCube = transform.parent.GetComponent<Cube>().LeftCube;
        }
        return neighborCube;
    }
}
