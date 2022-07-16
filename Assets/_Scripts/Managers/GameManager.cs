using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public eDirection GravityDirection = eDirection.Down;
	public Ball Ball { get; set; }
	//public GameObject World { }

	public eManagerStatus Status { get; set; }

	public void Startup()
	{
		Status = eManagerStatus.Started;
	}

	public void UpdateWorldGravity()
	{

	}
}
