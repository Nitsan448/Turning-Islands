using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : CubeFace
{
    protected override string SoundName { get; set; } = "Magnet";
    [SerializeField] private bool _tutorialMagnet = false;

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        ball.ChangeVelocity(Vector2.zero);
        ball.Animator.SetBool("InMagnet", true);
        if (Direction == eDirection.Bottom || Direction == eDirection.Top)
        {
            ball.Animator.SetBool("Horizontal", false);
        }

        if (!_tutorialMagnet)
        {
            Managers.Game.GameOver();
        }
    }
}