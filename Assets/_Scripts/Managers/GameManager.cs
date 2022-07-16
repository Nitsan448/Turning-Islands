using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour, IGameManager
{
    public eDirection GravityDirection = eDirection.Down;
	public Ball Ball { get; set; }
	//public GameObject World { }

	public bool GameStarted = false;

	public eManagerStatus Status { get; set; }

	public void Startup()
	{
		Status = eManagerStatus.Started;
	}

	public void StartGame()
	{
		Managers.GameManager.GameStarted = true;
		Camera.main.GetComponent<Camera2D>().enabled = false;
		Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
	}
}
