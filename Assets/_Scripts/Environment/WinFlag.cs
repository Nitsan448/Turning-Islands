using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlag : CubeFace
{
    protected override string SoundName { get; set; } = "Sploosh";

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        GetComponentInChildren<Animator>().Play("Sploosh");
        Managers.Game.ballsNotInFlag--;
        if (Managers.Game.ballsNotInFlag == 0)
        {
            Managers.Game.LevelWon();
        }
    }
}
