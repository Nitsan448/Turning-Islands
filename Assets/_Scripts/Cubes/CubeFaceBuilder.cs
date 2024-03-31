#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

public class CubeFaceBuilder : MonoBehaviour
{
    [Range(0, 2)] public int CreatedPortalIndex;
    private string _cubeFacesPrefabsFolder = "Assets/Prefabs/CubeFaces";
    private eDirection _cubeFaceDirection;

    public void CreatePortal()
    {
        ResetCubeFace();
        string portalColorName = CreatedPortalIndex switch
        {
            0 => "Red",
            1 => "Blue",
            _ => "Green"
        };
        InstantiateObjectGraphicsPrefab(_cubeFacesPrefabsFolder + "/Portals/" + portalColorName + "Portal.prefab");
        Portal createdPortal = gameObject.AddComponent<Portal>();
        SetPortalFields(createdPortal);
        gameObject.name = _cubeFaceDirection + ": Portal";
        FindObjectOfType<CubesManager>().ConnectAllPortalsAndButtons();
        EditorUtility.SetDirty(createdPortal);
    }

    private void ResetCubeFace()
    {
        _cubeFaceDirection = GetComponent<CubeFace>().Direction;
        DestroyExistingCubeFace();
        ChangeColliderHeight(0);
        if (transform.parent.GetComponentInChildren<WinFlag>() == null)
        {
            transform.parent.GetComponent<Cube>().IsSelectable = true;
        }
    }

    private void DestroyExistingCubeFace()
    {
        if (GetComponent<CubeFace>() != null)
        {
            DestroyImmediate(GetComponent<CubeFace>());
        }

        if (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private GameObject InstantiateObjectGraphicsPrefab(string prefabPath)
    {
        GameObject objectPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject createdObject = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;
        createdObject.transform.parent = transform;
        createdObject.transform.position = transform.position;
        if (_cubeFaceDirection == eDirection.Right || _cubeFaceDirection == eDirection.Left)
        {
            createdObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            createdObject.transform.localEulerAngles = Vector3.zero;
        }

        return createdObject;
    }

    private void SetPortalFields(Portal createdPortal)
    {
        createdPortal.Direction = _cubeFaceDirection;
        createdPortal.PortalIndex = CreatedPortalIndex;
        createdPortal.PortalGraphics = GetComponentInChildren<PortalGraphics>();
        MaterialPropertyBlock materialPropertyBlock = new();
        materialPropertyBlock.SetInt("_IsOpen", 1);
        createdPortal.PortalGraphics.PortalInside.SetPropertyBlock(materialPropertyBlock);
        createdPortal.PortalGraphics.PortalOutside.SetPropertyBlock(materialPropertyBlock);
        createdPortal.Collider = createdPortal.GetComponent<BoxCollider2D>();
    }

    public void CreatePortalButton()
    {
        ResetCubeFace();
        GameObject portalButtonGraphics = InstantiateObjectGraphicsPrefab(
            _cubeFacesPrefabsFolder + "/PortalButton.prefab"
        );
        PortalButton createdPortalButton = gameObject.AddComponent<PortalButton>();
        createdPortalButton.Direction = _cubeFaceDirection;
        createdPortalButton.PortalIndex = CreatedPortalIndex;

        Vector3 portalButtonRotation = portalButtonGraphics.transform.localEulerAngles;
        portalButtonGraphics.transform.localEulerAngles = new Vector3(
            portalButtonRotation.x,
            portalButtonRotation.y,
            portalButtonRotation.z - 90
        );
        ChangeColliderHeight(0.5f);
        ChangeGraphicsPosition(0.4f, portalButtonGraphics);
        createdPortalButton.GetComponentInChildren<SpriteRenderer>().color =
            PortalColors.ColorByIndex[CreatedPortalIndex];
        gameObject.name = _cubeFaceDirection + ": Portal Button";
        FindObjectOfType<CubesManager>().ConnectAllPortalsAndButtons();
        EditorUtility.SetDirty(createdPortalButton);
    }

    public void CreateBouncySurface(bool updateConnectedTube)
    {
        Tube tube = GetComponent<Tube>();
        if (tube != null && updateConnectedTube)
        {
            tube.ConnectedTube.GetComponent<CubeFaceBuilder>().CreateBouncySurface(false);
        }

        ResetCubeFace();
        BouncySurface createdBouncySurface = gameObject.AddComponent<BouncySurface>();
        createdBouncySurface.Direction = _cubeFaceDirection;
        gameObject.name = _cubeFaceDirection + ": Bouncy Surface";
        EditorUtility.SetDirty(createdBouncySurface);
    }

    public void CreateMagnet()
    {
        ResetCubeFace();
        GameObject magnetGraphics = InstantiateObjectGraphicsPrefab(
            _cubeFacesPrefabsFolder + "/Magnet.prefab"
        );

        Vector3 magnetRotation = magnetGraphics.transform.localEulerAngles;
        magnetGraphics.transform.localEulerAngles = new Vector3(
            magnetRotation.x,
            magnetRotation.y,
            magnetRotation.z + 90
        );
        ChangeColliderHeight(0.6f);
        GetComponent<BoxCollider2D>().isTrigger = true;

        Magnet createdMagnet = gameObject.AddComponent<Magnet>();
        createdMagnet.Direction = _cubeFaceDirection;
        gameObject.name = _cubeFaceDirection + ": Magnet";
        EditorUtility.SetDirty(createdMagnet);
    }

    private void ChangeColliderHeight(float newYOffset)
    {
        if (_cubeFaceDirection == eDirection.Right || _cubeFaceDirection == eDirection.Left)
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, -newYOffset);
        }
        else
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, newYOffset);
        }
    }

    public void CreateWinFlag()
    {
        ResetCubeFace();
        GameObject winFlagGraphics = InstantiateObjectGraphicsPrefab(
            _cubeFacesPrefabsFolder + "/WinFlag.prefab"
        );
        ChangeGraphicsPosition(0.8f, winFlagGraphics);
        // ChangeColliderHeight(-0.2f);
        transform.parent.GetComponent<Cube>().IsSelectable = false;

        WinFlag createdWinFlag = gameObject.AddComponent<WinFlag>();
        createdWinFlag.Direction = _cubeFaceDirection;
        gameObject.name = _cubeFaceDirection + ": Win flag";
        EditorUtility.SetDirty(createdWinFlag);
        EditorUtility.SetDirty(transform.parent.GetComponent<Cube>());
    }

    private void ChangeGraphicsPosition(float newYPosition, GameObject objectGraphics)
    {
        if (_cubeFaceDirection == eDirection.Right || _cubeFaceDirection == eDirection.Left)
        {
            objectGraphics.transform.localPosition = new Vector2(0, -newYPosition);
        }
        else
        {
            objectGraphics.transform.localPosition = new Vector2(0, newYPosition);
        }
    }

    public void CreateTube(bool startTubeOnThisFace, bool turned)
    {
        _cubeFaceDirection = GetComponent<CubeFace>().Direction;
        string tubePrefabPath = _cubeFacesPrefabsFolder + "/StraightTube.prefab";
        gameObject.name = _cubeFaceDirection + ": Straight Tube";
        if (turned)
        {
            tubePrefabPath = _cubeFacesPrefabsFolder + "/TurnedTube.prefab";
            gameObject.name = _cubeFaceDirection + ": Turned Tube";
        }

        Tube createdTube = CreateTubeOnThisFace();

        if (startTubeOnThisFace)
        {
            InstantiateObjectGraphicsPrefab(tubePrefabPath);
            Tube neighborTube = CreateTubeOnNeighborFace(turned);
            createdTube.ConnectedTube = neighborTube;
            neighborTube.ConnectedTube = createdTube;
        }

        createdTube.Turned = turned;
        EditorUtility.SetDirty(createdTube);
    }

    private Tube CreateTubeOnThisFace()
    {
        _cubeFaceDirection = GetComponent<CubeFace>().Direction;
        DestroyExistingCubeFace();
        Tube createdTube = gameObject.AddComponent<Tube>();
        createdTube.Direction = _cubeFaceDirection;
        return createdTube;
    }

    private Tube CreateTubeOnNeighborFace(bool turned)
    {
        eDirection neighborCubeDirection = DirectionExtensions.GetNewDirection(
            _cubeFaceDirection,
            1
        );
        if (!turned)
        {
            neighborCubeDirection = DirectionExtensions.GetNewDirection(neighborCubeDirection, 1);
        }

        CubeFaceBuilder neighborCubeBuilder = transform.parent
            .GetComponent<Cube>()
            .GetCubeFaceObjectByDirection(neighborCubeDirection)
            .GetComponent<CubeFaceBuilder>();
        neighborCubeBuilder.CreateTube(false, turned);
        Tube neighborTube = neighborCubeBuilder.transform.GetComponent<Tube>();
        return neighborTube;
    }

    public void CreateTrampoline(eDirection trampolineDirection)
    {
        ResetCubeFace();
        GameObject trampolineGraphics = InstantiateObjectGraphicsPrefab(
            _cubeFacesPrefabsFolder + "/Trampoline.prefab"
        );
        if (trampolineDirection == eDirection.Left)
        {
            trampolineGraphics.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        Trampoline createdTrampoline = gameObject.AddComponent<Trampoline>();
        createdTrampoline.TrampolineDirection = trampolineDirection;
        createdTrampoline.Direction = _cubeFaceDirection;
        gameObject.name = _cubeFaceDirection + ": Trampoline";

        ChangeColliderHeight(0.8f);
    }

    public void CreateBall()
    {
        _cubeFaceDirection = GetComponent<CubeFace>().Direction;
        GameObject ballPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(
            "Assets/Prefabs/Ball.prefab"
        );
        FindObjectOfType<GameManager>().BallsNotInFlag++;
        EditorUtility.SetDirty(FindObjectOfType<GameManager>());
        GameObject ball = PrefabUtility.InstantiatePrefab(ballPrefab) as GameObject;
        float ballPositionOffSetAmount = FindObjectOfType<CubesManager>().DistanceBetweenCubes.x / 2 - 0.5f;
        Vector3 ballPostionOffset = Vector3.zero;
        switch (_cubeFaceDirection)
        {
            case eDirection.Top:
                ballPostionOffset = new Vector2(0, ballPositionOffSetAmount);
                ball.GetComponent<Ball>().StartingVelocity = new Vector2(0, -1);
                break;
            case eDirection.Right:
                ballPostionOffset = new Vector2(ballPositionOffSetAmount, 0);
                ball.GetComponent<Ball>().StartingVelocity = new Vector2(-1, 0);
                break;
            case eDirection.Bottom:
                ballPostionOffset = new Vector2(0, -ballPositionOffSetAmount);
                ball.GetComponent<Ball>().StartingVelocity = new Vector2(0, 1);
                break;
            case eDirection.Left:
                ballPostionOffset = new Vector2(-ballPositionOffSetAmount, 0);
                ball.GetComponent<Ball>().StartingVelocity = new Vector2(1, 0);
                break;
        }

        ball.GetComponent<Ball>().SetVelocityIndicatorRotation();
        ball.transform.position = transform.parent.position + ballPostionOffset;
    }

    public void CreateCubeTurner()
    {
        ResetCubeFace();
        GameObject cubeTurnerGraphics = InstantiateObjectGraphicsPrefab(
            _cubeFacesPrefabsFolder + "/CubeTurner.prefab"
        );
        // cubeTurnerGraphics.transform.rotation = Quaternion.Euler(new Vector3(cubeTurnerGraphics.transform.rotation.x,
        //     cubeTurnerGraphics.transform.rotation.y, cubeTurnerGraphics.transform.rotation.z));

        CubeTurner createdCubeTurner = gameObject.AddComponent<CubeTurner>();
        createdCubeTurner.Direction = _cubeFaceDirection;
        gameObject.name = _cubeFaceDirection + ": Cube Turner";

        ChangeColliderHeight(0.2f);
    }
}
#endif