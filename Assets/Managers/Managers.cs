using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static GameManager Game { get; private set; }
    public static UIManager UI { get; private set; }
    public static CubesManager Cubes { get; private set; }
    public static AudioManager Audio { get; private set; }
    private List<IGameManager> startSequence;

    void Awake()
    {
        GetManagers();
        SetStartSequenceOrder();
        StartUpManagers();
    }

    public void GetManagers()
    {
        Game = GetComponentInChildren<GameManager>();
        UI = GetComponentInChildren<UIManager>();
        Audio = GetComponentInChildren<AudioManager>();
        Cubes = FindObjectOfType<CubesManager>();
    }

    private void SetStartSequenceOrder()
    {
        startSequence = new List<IGameManager>();
        startSequence.Add(Game);
        startSequence.Add(UI);
        startSequence.Add(Cubes);
        startSequence.Add(Audio);
    }

    private void StartUpManagers()
    {
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
    }
}
