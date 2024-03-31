using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour, IGameManager
{
    public Cube SelectedCube;
    private AudioSource _selectionAudio;
    public Vector2 DistanceBetweenCubes;
    [SerializeField] private LayerMask _cubeLayerMask;
    private Cube _previousHoveredCube;

    [Header("Mouse Input")] [SerializeField]
    private bool _allowMouseInput;

    [SerializeField] private bool _hoverToSelect;

    public void Startup()
    {
        _selectionAudio = GetComponent<AudioSource>();
        SelectedCube.SelectedSprite.SetActive(true);
    }

    void Update()
    {
        if (Managers.Game.GameState == EGameState.Editing)
        {
            HandleKeyboardInputs();
            if (_allowMouseInput)
            {
                HandleMouseInputs();
            }
        }
    }

    private void HandleKeyboardInputs()
    {
        HandleKeyboardSelectionInput();
        HandleKeyboardRotationInput();
    }

    private void HandleKeyboardSelectionInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            FindCubeToSelect(SelectedCube, eDirection.Top);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            FindCubeToSelect(SelectedCube, eDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            FindCubeToSelect(SelectedCube, eDirection.Bottom);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
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
            cubeToSelect.SelectedSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            _previousHoveredCube = null;
        }
    }

    private void HandleKeyboardRotationInput()
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

    private void HandleMouseInputs()
    {
        HandleMouseSelectionInput();
        HandleMouseRotationInput();
        HandleMouseHover();
    }


    private void HandleMouseSelectionInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Cube cubeToSelect = GetHoveredCube();
        if (cubeToSelect == null || !cubeToSelect.IsSelectable) return;

        UpdateSelectedCube(cubeToSelect);
    }

    private void HandleMouseRotationInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectedCube.RotateCube(eDirection.Right);
        }

        if (_hoverToSelect && Input.GetMouseButtonDown(0))
        {
            SelectedCube.RotateCube(eDirection.Left);
        }
    }


    private void HandleMouseHover()
    {
        Cube hoveredCube = GetHoveredCube();
        if (hoveredCube == null || hoveredCube != _previousHoveredCube)
        {
            ResetHoveredCube();
        }

        if (hoveredCube != null && hoveredCube.IsSelectable && hoveredCube != SelectedCube)
        {
            hoveredCube.SelectedSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            hoveredCube.SelectedSprite.SetActive(true);
            _previousHoveredCube = hoveredCube;
            if (_hoverToSelect)
            {
                UpdateSelectedCube(hoveredCube);
            }
        }
    }

    private Cube GetHoveredCube()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
            distance: Mathf.Infinity,
            layerMask: _cubeLayerMask);
        if (!hit) return null;

        Cube hoveredCube = hit.transform.parent.GetComponent<Cube>();
        return hoveredCube;
    }

    private void ResetHoveredCube()
    {
        if (_previousHoveredCube == null && SelectedCube != _previousHoveredCube) return;
        _previousHoveredCube.SelectedSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        _previousHoveredCube.SelectedSprite.SetActive(false);
        _previousHoveredCube = null;
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
            if (portals[i].ConnectedPortal != null)
            {
                portals[i].ConnectedPortal.ConnectedPortal = portals[i];
            }
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