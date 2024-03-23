using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : CubeFace
{
    protected override string SoundName { get; set; } = "Magnet";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        ball.ChangeVelocity(Vector2.zero);
        ball.Animator.SetBool("InMagnet", true);
        if (Direction == eDirection.Bottom || Direction == eDirection.Top)
        {
            ball.Animator.SetBool("Horizontal", false);
        }

        Managers.Game.GameOver();
    }
}