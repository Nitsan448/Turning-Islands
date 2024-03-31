using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButtonsTutorial : Tutorial
{
    protected override void ResetTutorialState()
    {
        base.ResetTutorialState();
        GetComponentInChildren<Portal>().ChangeOpenState(true);
    }
}