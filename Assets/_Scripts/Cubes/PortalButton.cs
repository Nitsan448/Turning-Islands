using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : CubeFace
{
    public Portal ConnectedPortal;

    public int PortalIndex = -1;
    protected override string SoundName { get; set; } = "No Sound";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        ConnectedPortal.ChangeOpenState();
        ConnectedPortal.ConnectedPortal.ChangeOpenState();
        ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
        ball.GetComponent<Animator>().Play("Squish");
    }
}
