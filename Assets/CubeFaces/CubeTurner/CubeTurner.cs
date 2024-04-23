using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTurner : CubeFace
{
    protected override string SoundName { get; set; }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
        ball.Animator.Play("Squish");
        transform.parent.GetComponent<Cube>().RotateCube(EDirection.Right);
    }
}