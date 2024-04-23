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
    public GameObject CantSelectSprite;

    [SerializeField] private float _rotationTime = 0.5f;

    private bool _coroutineActive = false;
    private CubeFace[] _cubeFaces;
    private Animator _animator;

    public bool IsSelectable = true;

    private void Awake()
    {
        _cubeFaces = GetComponentsInChildren<CubeFace>();
        _animator = GetComponent<Animator>();
    }

    public void RotateCube(EDirection direction)
    {
        if (!_coroutineActive)
        {
            StartCoroutine(UpdateCubeRotationCoroutine(direction));
        }
    }

    private IEnumerator UpdateCubeRotationCoroutine(EDirection direction)
    {
        _coroutineActive = true;
        float currentBlend = _animator.GetFloat("Blend");
        int blendAdditionSign = currentBlend == 1 ? -1 : 1;

        Managers.Audio.PlaySound("CubeRotation");
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = GetTargetRotation(direction);
        float currentTime = 0;

        while (currentTime < _rotationTime)
        {
            transform.rotation = Quaternion.Lerp(
                currentRotation,
                targetRotation,
                currentTime / _rotationTime
            );
            currentTime += Time.deltaTime;
            _animator.SetFloat("Blend", currentTime / _rotationTime * blendAdditionSign);
            yield return null;
        }

        transform.rotation = targetRotation;
        UpdateCubeFacesDirection(direction);

        _animator.SetFloat("Blend", blendAdditionSign == 1 ? 1 : 0);
        _coroutineActive = false;
        yield return null;
    }

    private Quaternion GetTargetRotation(EDirection direction)
    {
        int zRotationIncrement = 90;
        if (direction == EDirection.Right)
        {
            zRotationIncrement = -90;
        }

        return Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            (transform.eulerAngles.z + zRotationIncrement) % 360
        );
    }

    private void UpdateCubeFacesDirection(EDirection direction)
    {
        foreach (CubeFace cubeFace in _cubeFaces)
        {
            cubeFace.UpdateDirection(direction);
        }
    }

    public GameObject GetCubeFaceObjectByDirection(EDirection direction)
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

    public IEnumerator ShowCantSelectSpriteForDuration(float duration)
    {
        CantSelectSprite.SetActive(true);
        yield return new WaitForSeconds(duration);
        CantSelectSprite.SetActive(false);
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

    public void Turn(EDirection turnDirection)
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
            Transform childTransform = transform.GetChild(i);
            CubeFace cubeFace = i == 0 ? cubeFaces[3] : cubeFaces[i - 1];
            SetChildFace(childTransform, cubeFace);
            if (cubeFace is Tube && ((Tube)cubeFace).Turned)
            {
                //Very bad fix, only works when tube is not on left side
                //Better to live with it?
                i++;
            }
        }
    }

    private void SetChildFace(Transform child, CubeFace cubeFace)
    {
        if (cubeFace is Portal)
        {
            child.GetComponent<CubeFaceBuilder>().CreatedPortalIndex =
                ((Portal)cubeFace).PortalIndex;
            child.GetComponent<CubeFaceBuilder>().CreatePortal();
        }
        else if (cubeFace is PortalButton)
        {
            child.GetComponent<CubeFaceBuilder>().CreatedPortalIndex =
                ((PortalButton)cubeFace).PortalIndex;
            child.GetComponent<CubeFaceBuilder>().CreatePortalButton();
        }
        else if (cubeFace is Tube)
        {
            //This doesn't work with turned tubes, since they take two faces
            child.GetComponent<CubeFaceBuilder>().CreateTube(true, ((Tube)cubeFace).Turned);
        }
        else if (cubeFace is Trampoline)
        {
            child
                .GetComponent<CubeFaceBuilder>()
                .CreateTrampoline((cubeFace as Trampoline).TrampolineDirection);
        }
        else if (cubeFace is BouncySurface)
        {
            child.GetComponent<CubeFaceBuilder>().CreateBouncySurface(true);
        }
        else if (cubeFace is CubeTurner)
        {
            child.GetComponent<CubeFaceBuilder>().CreateCubeTurner();
        }
        else if (cubeFace is Magnet)
        {
            child.GetComponent<CubeFaceBuilder>().CreateMagnet();
        }
        else if (cubeFace is WinFlag)
        {
            child.GetComponent<CubeFaceBuilder>().CreateWinFlag();
        }
    }
#endif
}