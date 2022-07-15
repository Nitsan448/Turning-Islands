using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    eManagerStatus Status { get; }
    void Startup();
}
