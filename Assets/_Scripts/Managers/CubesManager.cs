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
        PortalButton[] portalButtons = GetComponentsInChildren<PortalButton>();
        Portal[] portals = GetComponentsInChildren<Portal>();

        for (int i = 0; i < portals.Length; i++)
        {
            int portalIndex = portals[i].PortalIndex;
            portals[i].PortalIndex = -1;
            portals[i].ConnectedPortal = FindPortalByIndex(portalIndex, portals);
            portals[i].PortalIndex = portalIndex;
        }

        for (int i = 0; i < portalButtons.Length; i++)
        {
            int portalIndex = portalButtons[i].PortalIndex;
            portalButtons[i].ConnectedPortal = FindPortalByIndex(portalIndex, portals);
        }
    }

    private Portal FindPortalByIndex(int portalIndex, Portal[] portals)
    {
        foreach (Portal portal in GetComponentsInChildren<Portal>())
        {
            if (portal.PortalIndex == portalIndex)
            {
                return portal;
            }
        }
        return null;
    }
}
