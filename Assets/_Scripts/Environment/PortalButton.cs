using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : CubeFace
{
    public Portal ConnectedPortal;
    private float timeUntilNextCollisionPossible;
    public int PortalIndex = -1;
    protected override string SoundName { get; set; } = "No Sound";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        if (timeUntilNextCollisionPossible <= 0)
        {
            ConnectedPortal.ChangeOpenState(true);
            ball.ChangeVelocity(GetComponent<CubeFace>().GetVelocity());
            ball.GetComponent<Animator>().Play("Squish");
            timeUntilNextCollisionPossible = 0.5f;
        }
    }

    private void Update()
    {
        if (timeUntilNextCollisionPossible > 0)
        {
            timeUntilNextCollisionPossible -= Time.deltaTime;
        }
    }
}
