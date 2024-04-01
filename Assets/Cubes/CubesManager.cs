using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubesManager : MonoBehaviour, IGameManager
{
    public Cube SelectedCube;
    public Vector2 DistanceBetweenCubes;
    [SerializeField] private LayerMask _cubeLayerMask;
    private Cube _previousHoveredCube;

    [Header("Mouse Input")] [SerializeField]
    private bool _allowMouseInput;

    public bool Simulation = false;
    public bool SelectedCubeChanged = false;
    public bool CubeRotated = false;
    private bool _allowSelectionUsingMouse = true;

    public void Startup()
    {
        if (Simulation) return;
        SelectedCube.SelectedSprite.SetActive(true);
    }

    void Update()
    {
        if (Simulation) return;
        if (Managers.Game.GameState == EGameState.Editing)
        {
            if (_allowMouseInput)
            {
                HandleMouseInputs();
            }

            HandleKeyboardInputs();
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
            _allowSelectionUsingMouse = false;
            if (adjacentCube.IsSelectable)
            {
                UpdateSelectedCube(adjacentCube);
            }
            else
            {
                adjacentCube.StartCoroutine(adjacentCube.ShowCantSelectSpriteForDuration(0.1f));
                FindCubeToSelect(adjacentCube, direction);
            }
        }
    }

    private void UpdateSelectedCube(Cube cubeToSelect)
    {
        if (cubeToSelect == null) return;

        if (cubeToSelect != SelectedCube)
        {
            SelectedCubeChanged = true;
        }

        SelectedCube.SelectedSprite.SetActive(false);
        Managers.Audio.PlaySound("CubeSelection");
        SelectedCube = cubeToSelect;
        cubeToSelect.SelectedSprite.SetActive(true);
        cubeToSelect.SelectedSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        if (_previousHoveredCube != null)
        {
            _previousHoveredCube.CantSelectSprite.SetActive(false);
            _previousHoveredCube = null;
        }
    }

    private void HandleKeyboardRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateCube(SelectedCube, eDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateCube(SelectedCube, eDirection.Right);
        }
    }

    private void RotateCube(Cube cube, eDirection direction)
    {
        CubeRotated = true;
        cube.RotateCube(direction);
    }

    private void HandleMouseInputs()
    {
        HandleMouseRotationInput();
        HandleMouseSelection();
    }


    private void HandleMouseRotationInput()
    {
        if (UIExtensions.IsOverUI()) return;
        if (Input.GetMouseButtonDown(0))
        {
            RotateCube(SelectedCube, eDirection.Left);
        }

        if (Input.GetMouseButtonDown(1))
        {
            RotateCube(SelectedCube, eDirection.Right);
        }
    }


    private void HandleMouseSelection()
    {
        Cube hoveredCube = GetHoveredCube();
        _allowSelectionUsingMouse |= hoveredCube == null;
        if (!_allowSelectionUsingMouse)
        {
            return;
        }

        if (hoveredCube == null)
        {
            if (_previousHoveredCube != null)
            {
                _previousHoveredCube.CantSelectSprite.SetActive(false);
                _previousHoveredCube = null;
            }
        }
        else if (!hoveredCube.IsSelectable)
        {
            hoveredCube.CantSelectSprite.SetActive(true);
        }
        else if (hoveredCube != SelectedCube)
        {
            UpdateSelectedCube(hoveredCube);
        }

        _previousHoveredCube = hoveredCube;
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

    [Button]
    public void ResetAllCubeFaces()
    {
        foreach (CubeFaceBuilder cubeFaceBuilder in GetComponentsInChildren<CubeFaceBuilder>())
        {
            cubeFaceBuilder.CreateBouncySurface(false);
        }
    }
}