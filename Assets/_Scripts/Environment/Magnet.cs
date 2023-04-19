using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : CubeFace
{
    private bool ballHitMagnet;
    private float currentTime = 0;
    private float timeInterval = 0.3f;

    // private int velocitySign = 1;
    private Ball _ball;
    protected override string SoundName { get; set; } = "Magnet";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        _ball = ball;
        ball.ChangeVelocity(Vector2.zero);
        ballHitMagnet = true;
        Managers.Game.GameOver();
    }

    private void FixedUpdate()
    {
        if (ballHitMagnet)
        {
            //TODO: Do this using normal animation!
            BallInMagnetAnimation();
        }
    }

    private void BallInMagnetAnimation()
    {
        if (currentTime > timeInterval)
        {
            // _ball.ChangeVelocity(
            // GetComponent<CubeFace>().GetVelocity().normalized * 0.4f * velocitySign
            // );
            // velocitySign *= -1;
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
