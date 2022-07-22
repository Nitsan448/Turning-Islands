using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CubeFaceBuilder : MonoBehaviour
{
	private string cubeFacesPrefabsFolder = "Assets/Prefabs/CubeFaces";
	[SerializeField] private int _createdPortalIndex;
	public void CreatePortal()
	{
		eDirection cubeFaceDirection = GetComponent<CubeFace>().Direction;
		if (GetComponent<CubeFace>() != null)
		{
			DestroyImmediate(GetComponent<CubeFace>());
		}
		GameObject portalGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(cubeFacesPrefabsFolder + "/portal");
		portalGameObject.transform.parent = transform;
		if (cubeFaceDirection == eDirection.Right || cubeFaceDirection == eDirection.Down)
		{
			portalGameObject.transform.eulerAngles = new Vector3(0, 0, 180);
		}
		Portal createdPortal = gameObject.AddComponent<Portal>();
		//createdPortal.GetComponentInChildren<SpriteRenderer>().color = _createdPortalColor;
		createdPortal.PortalIndex = -1;
		foreach (Portal portal in FindObjectsOfType<Portal>())
		{
			if (portal.PortalIndex == _createdPortalIndex)
			{
				createdPortal.ConnectedPortal = portal;
				portal.ConnectedPortal.ConnectedPortal = createdPortal;
				//createdPortal.GetComponentInChildren<SpriteRenderer>().color =
					//portal.ConnectedPortal.GetComponentInChildren<SpriteRenderer>().color;
			}
		}
		createdPortal.PortalIndex = _createdPortalIndex;
	}
}
