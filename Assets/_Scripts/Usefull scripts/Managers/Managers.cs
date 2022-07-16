using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static GameManager GameManager { get; private set; }

    private List<IGameManager> startSequence;

    void Awake()
    {
        //Application.targetFrameRate = 60;
        GetManagers();
        SetStartSequenceOrder();
        StartupManagers();
        Debug.Log("All managers started up");
    }

    private void GetManagers()
	{
        GameManager = GetComponentInChildren<GameManager>();
    }

    private void SetStartSequenceOrder()
	{
        startSequence = new List<IGameManager>();
        startSequence.Add(GameManager);
    }

    private void StartupManagers()
	{
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
	}
}
