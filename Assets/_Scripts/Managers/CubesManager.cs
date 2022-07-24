using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour, IGameManager
{
	public Cube SelectedCube;
	private AudioSource _selectionAudio;

	public void Startup()
	{
		_selectionAudio = GetComponent<AudioSource>();
		SelectedCube.SelectedSprite.SetActive(true);
		ConnectAllPortals();
	}

	public void ChangeEffectorsState(bool newState)
	{
		foreach (Cube cube in GetComponentsInChildren<Cube>())
		{
			cube.GetComponent<PointEffector2D>().enabled = newState;
		}
	}

	void Update()
    {
		if (Managers.Game.GameState == eGameState.Editing)
		{
			HandleSelectionInput();
			HandleRotationInput();
		}
	}

	private void HandleSelectionInput()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpdateSelectedCube(SelectedCube.TopCube);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			UpdateSelectedCube(SelectedCube.RightCube);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			UpdateSelectedCube(SelectedCube.LeftCube);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			UpdateSelectedCube(SelectedCube.BottomCube);
		}
	}

	private void UpdateSelectedCube(Cube cubeToSelect)
	{
		if (cubeToSelect != null)
		{
			SelectedCube.SelectedSprite.SetActive(false);
			Managers.Audio.PlaySound("CubeSelection");
			SelectedCube = cubeToSelect;
			cubeToSelect.SelectedSprite.SetActive(true);
		}
	}

	private void HandleRotationInput()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SelectedCube.RotateCube(eDirection.Left);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			SelectedCube.RotateCube(eDirection.Right);
		}
	}

	private void ConnectAllPortals()
	{
		Portal[] portals = GetComponentsInChildren<Portal>();
		for(int i = 0; i < portals.Length; i++)
		{
			int portalIndex = portals[i].PortalIndex;
			portals[i].PortalIndex = -1;
			portals[i].ConnectedPortal = findPortalByIndex(portalIndex, portals);
			portals[i].PortalIndex = portalIndex;
		}
	}

	private Portal findPortalByIndex(int portalIndex, Portal[] portals)
	{
		foreach(Portal portal in GetComponentsInChildren<Portal>())
		{
			if(portal.PortalIndex == portalIndex)
			{
				return portal;
			}
		}
		return null;
	}
}