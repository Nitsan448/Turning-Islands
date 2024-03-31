using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : CubeFace
{
    protected override string SoundName { get; set; } = "Bounce";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
        ball.Animator.Play("Squish");
        if (transform.parent.GetComponent<DestructibleCube>() != null)
        {
            transform.parent.GetComponent<DestructibleCube>().TakeDamage();
        }
    }
}