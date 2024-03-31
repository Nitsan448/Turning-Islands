using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    public Cube RightCube;
    public Cube LeftCube;
    public Cube TopCube;
    public Cube BottomCube;

    public GameObject SelectedSprite;

    [SerializeField] private float _rotationTime = 0.5f;

    private bool _coroutineActive = false;
    private CubeFace[] _cubeFaces;

    public bool IsSelectable = true;

    private void Awake()
    {
        _cubeFaces = GetComponentsInChildren<CubeFace>();
    }

    public void RotateCube(eDirection direction)
    {
        if (!_coroutineActive)
        {
            StartCoroutine(UpdateCubeRotationCoroutine(direction));
        }
    }

    private IEnumerator UpdateCubeRotationCoroutine(eDirection direction)
    {
        _coroutineActive = true;

        Managers.Audio.PlaySound("CubeRotation");
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = getTargetRotation(direction);
        float currentTime = 0;

        while (currentTime < _rotationTime)
        {
            transform.rotation = Quaternion.Lerp(
                currentRotation,
                targetRotation,
                currentTime / _rotationTime
            );
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        UpdateCubeFacesDirection(direction);

        _coroutineActive = false;
        yield return null;
    }

    private Quaternion getTargetRotation(eDirection direction)
    {
        int zRotationIncrement = 90;
        if (direction == eDirection.Right)
        {
            zRotationIncrement = -90;
        }

        return Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            (transform.eulerAngles.z + zRotationIncrement) % 360
        );
    }

    private void UpdateCubeFacesDirection(eDirection direction)
    {
        foreach (CubeFace cubeFace in _cubeFaces)
        {
            cubeFace.UpdateDirection(direction);
        }
    }

    public GameObject GetCubeFaceObjectByDirection(eDirection direction)
    {
        foreach (CubeFace cubeFace in GetComponentsInChildren<CubeFace>())
        {
            if (direction == cubeFace.Direction)
            {
                return cubeFace.gameObject;
            }
        }

        return null;
    }
#if UNITY_EDITOR
    public void DeleteCube()
    {
        if (RightCube)
        {
            RightCube.LeftCube = null;
        }

        if (BottomCube)
        {
            BottomCube.TopCube = null;
        }

        if (LeftCube)
        {
            LeftCube.RightCube = null;
        }

        if (TopCube)
        {
            TopCube.BottomCube = null;
        }

        DestroyImmediate(gameObject);
    }

    public void Turn(eDirection turnDirection)
    {
        CubeFace[] cubeFaces = new CubeFace[]
        {
            transform.GetChild(0).GetComponent<CubeFace>(),
            transform.GetChild(1).GetComponent<CubeFace>(),
            transform.GetChild(2).GetComponent<CubeFace>(),
            transform.GetChild(3).GetComponent<CubeFace>()
        };

        for (int i = 0; i < 4; i++)
        {
            Transform childTransform = transform.GetChild(i + 1);
            if (i == 3)
            {
                childTransform = transform.GetChild(0);
            }

            if (cubeFaces[i] is Portal)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreatePortal();
            }
            else if (cubeFaces[i] is PortalButton)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreatePortalButton();
            }
            else if (cubeFaces[i] is Tube)
            {
                //This doesn't work with tubes, since they take two faces
                childTransform
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTube(true, (cubeFaces[i] as Tube).Turned);
            }
            else if (cubeFaces[i] is Trampoline)
            {
                childTransform
                    .GetComponent<CubeFaceBuilder>()
                    .CreateTrampoline((cubeFaces[i] as Trampoline).TrampolineDirection);
            }
            else if (cubeFaces[i] is BouncySurface)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreateBouncySurface(true);
            }
            else if (cubeFaces[i] is CubeTurner)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreateCubeTurner();
            }
            else if (cubeFaces[i] is Magnet)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreateMagnet();
            }
            else if (cubeFaces[i] is WinFlag)
            {
                childTransform.GetComponent<CubeFaceBuilder>().CreateWinFlag();
            }
        }
    }
#endif
}