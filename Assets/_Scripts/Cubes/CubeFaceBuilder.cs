using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CubeFaceBuilder : MonoBehaviour
{
	[SerializeField] private bool _portalOpenState;

	[Range(0, 4)]
	[SerializeField] private int _createdPortalIndex;
	private string _cubeFacesPrefabsFolder = "Assets/Prefabs/CubeFaces";
	private eDirection _cubeFaceDirection;

	public void CreatePortal()
	{
		_cubeFaceDirection = GetComponent<CubeFace>().Direction;
		DestroyExistingCubeFace();
		InstantiateObjectGraphicsPrefab(_cubeFacesPrefabsFolder + "/Portal.prefab");
		Portal createdPortal = gameObject.AddComponent<Portal>();
		SetPortalFields(createdPortal);
		ConnectPortal(createdPortal);
	}

	private void DestroyExistingCubeFace()
	{
		if (GetComponent<CubeFace>() != null)
		{
			DestroyImmediate(GetComponent<CubeFace>());
		}
		if(transform.childCount > 0)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}

	private GameObject InstantiateObjectGraphicsPrefab(string prefabPath)
	{
		GameObject prefabToInstantiate = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
		GameObject CreatedObject = PrefabUtility.InstantiatePrefab(prefabToInstantiate) as GameObject;
		CreatedObject.transform.parent = transform;
		CreatedObject.transform.position = transform.position;
		if (_cubeFaceDirection == eDirection.Right || _cubeFaceDirection == eDirection.Left)
		{
			CreatedObject.transform.localEulerAngles = new Vector3(0, 0, 180);
		}
		else
		{
			CreatedObject.transform.localEulerAngles = Vector3.zero;
		}
		return CreatedObject;
	}

	private void ConnectPortal(Portal createdPortal)
	{
		CubesManager cubesManager = FindObjectOfType<CubesManager>();
		if (cubesManager.GetComponentsInChildren<Portal>().Length > 1)
		{
			foreach (Portal portal in cubesManager.GetComponentsInChildren<Portal>())
			{
				if (portal.PortalIndex == _createdPortalIndex)
				{
					createdPortal.ConnectedPortal = portal;
					portal.ConnectedPortal = createdPortal;
					if(portal.IsOpen != createdPortal.IsOpen)
					{
						portal.ChangeOpenState();
					}
				}
			}
		}
	}

	private void SetPortalFields(Portal createdPortal)
	{
		createdPortal.Direction = _cubeFaceDirection;
		if(_portalOpenState == false)
		{
			createdPortal.ChangeOpenState();
		}
		createdPortal.IsTube = false;
		createdPortal.PortalIndex = _createdPortalIndex;
		createdPortal.GetComponentInChildren<SpriteRenderer>().color = PortalColors.ColorByIndex[_createdPortalIndex];
	}

	public void CreateBouncySurface()
	{

	}
	public void CreateMagnet()
	{

	}
	public void CreateWinFlag()
	{

	}
	public void CreateTurnedTube()
	{

	}
	public void CreateStraightTube()
	{

	}
}
