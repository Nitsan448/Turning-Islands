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

        Vector2 targetPosition = getTargetPosition(ball);

        ball.GetComponent<Animator>().Play("Squish");
        StartCoroutine(ball.MoveTowardInArc(8, targetPosition, Direction));
    }

    private Vector2 getTargetPosition(Ball ball)
    {
        Cube neighborCube = FindNeighborCube();

        if (neighborCube == null)
        {
            // TODO: get position trampoline should send to even if neighbor cube is null
            Managers.Game.GameOver();
            return ball.transform.position;
        }

        Vector2 targetPosition = neighborCube
            .GetCubeFaceObjectByDirection(Direction)
            .transform.position;

        Vector2 targetFaceColliderOffset = neighborCube
            .GetCubeFaceObjectByDirection(Direction)
            .GetComponent<BoxCollider2D>()
            .offset;

        switch (Direction)
        {
            case eDirection.Top:
                targetPosition.y += targetFaceColliderOffset.y;
                break;
            case eDirection.Right:
                targetPosition.x += targetFaceColliderOffset.y;
                break;
            case eDirection.Bottom:
                targetPosition.y -= targetFaceColliderOffset.y;
                break;
            case eDirection.Left:
                targetPosition.x -= targetFaceColliderOffset.y;
                break;
        }
        return targetPosition;
    }

    private Cube FindNeighborCube()
    {
        Dictionary<(eDirection, eDirection), Cube> neighborMap = new Dictionary<
            (eDirection, eDirection),
            Cube
        >
        {
            { (eDirection.Right, eDirection.Top), transform.parent.GetComponent<Cube>().RightCube },
            {
                (eDirection.Left, eDirection.Bottom),
                transform.parent.GetComponent<Cube>().RightCube
            },
            { (eDirection.Left, eDirection.Top), transform.parent.GetComponent<Cube>().LeftCube },
            {
                (eDirection.Right, eDirection.Bottom),
                transform.parent.GetComponent<Cube>().LeftCube
            },
            {
                (eDirection.Left, eDirection.Left),
                transform.parent.GetComponent<Cube>().BottomCube
            },
            {
                (eDirection.Right, eDirection.Right),
                transform.parent.GetComponent<Cube>().BottomCube
            },
            { (eDirection.Right, eDirection.Left), transform.parent.GetComponent<Cube>().TopCube },
            { (eDirection.Left, eDirection.Right), transform.parent.GetComponent<Cube>().TopCube },
        };

        Cube neighborCube = neighborMap[(TrampolineDirection, Direction)];
        return neighborCube;
    }
}
