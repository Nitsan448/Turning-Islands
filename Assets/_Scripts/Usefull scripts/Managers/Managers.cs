using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static ExampleManager Example { get; private set; }

    private List<IGameManager> startSequence;

    void Awake()
    {
        Application.targetFrameRate = 60;
        GetManagers();
        SetStartSequenceOrder();
        StartupManagers();
        Debug.Log("All managers started up");
        //StartCoroutine(StartupManagers());
    }

    private void GetManagers()
	{
        Example = GetComponentInChildren<ExampleManager>();
    }

    private void SetStartSequenceOrder()
	{
        startSequence = new List<IGameManager>();
        startSequence.Add(Example);
    }

    private void StartupManagers()
	{
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
	}
}
