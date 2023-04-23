using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour, IGameManager
{
    public Cube SelectedCube;
    private AudioSource _selectionAudio;
    public Vector2 DistanceBetweenCubes;

    public void Startup()
    {
        _selectionAudio = GetComponent<AudioSource>();
        SelectedCube.SelectedSprite.SetActive(true);
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
            FindCubeToSelect(SelectedCube, eDirection.Top);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FindCubeToSelect(SelectedCube, eDirection.Right);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FindCubeToSelect(SelectedCube, eDirection.Bottom);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindCubeToSelect(SelectedCube, eDirection.Left);
        }
    }

    private void FindCubeToSelect(Cube cube, eDirection direction)
    {
        Cube adjacentCube = CubeExtensions.FindAdjacentCubeByDirection(cube, direction);

        if (adjacentCube != null)
        {
            if (adjacentCube.IsSelectable)
            {
                UpdateSelectedCube(adjacentCube);
            }
            else
            {
                FindCubeToSelect(adjacentCube, direction);
            }
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

    public void ConnectAllPortalsAndButtons()
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
