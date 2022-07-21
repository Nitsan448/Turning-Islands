using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFace : MonoBehaviour
{
	[SerializeField] private float _velocityWhenLeavingFace = 10;
	[SerializeField] private eDirection _direction;

	[HeaderAttribute("Portals")]
	[SerializeField] private Color _createdPortalColor;
	[SerializeField] private int _createdPortalIndex;
	private AudioSource _audioSource;

	private void Awake()
	{
		if(GetComponent<BouncyObject>() != null)
		{
			_audioSource = GetComponent<AudioSource>();
		}
		else
		{
			Destroy(GetComponent<AudioSource>());
			_audioSource = GetComponentInChildren<AudioSource>();
		}
	}

	public Vector2 GetVelocity()
	{
		switch (_direction)
		{
			case eDirection.Up:
				return new Vector2(0, _velocityWhenLeavingFace);
			case eDirection.Down:
				return new Vector2(0, -_velocityWhenLeavingFace);
			case eDirection.Right:
				return new Vector2(_velocityWhenLeavingFace, 0);
			case eDirection.Left:
				return new Vector2(-_velocityWhenLeavingFace, 0);
		}
		return Vector2.zero;
	}

	public void UpdateDirection(eDirection direction)
	{
		_direction = DirectionExtensions.GetNewDirection(_direction, DirectionExtensions.GetIntByDirection(direction));
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Managers.Game.ResetTimeUntilGameOver();
		if(GetComponentInChildren<AudioSource>() != null)
		{
			GetComponentInChildren<AudioSource>().Play();
		}
	}

	public void CreatePortal()
	{
		if (GetComponent<BouncyObject>() != null)
		{
			DestroyImmediate(GetComponent<BouncyObject>());
		}
		if (GetComponent<AudioSource>() != null)
		{
			DestroyImmediate(GetComponent<AudioSource>());
		}
		GameObject portalGameObject = Instantiate(FindObjectOfType<PrefabManager>().Portal, transform);
		if(_direction == eDirection.Right || _direction == eDirection.Down)
		{
			portalGameObject.transform.eulerAngles = new Vector3(0, 0, 180);
		}
		Portal createdPortal = gameObject.AddComponent<Portal>();
		createdPortal.GetComponentInChildren<SpriteRenderer>().color = _createdPortalColor;
		createdPortal.PortalIndex = -1;
		foreach (Portal portal in FindObjectsOfType<Portal>())
		{
			if(portal.PortalIndex == _createdPortalIndex)
			{
				createdPortal.ConnectedPortal = portal;
				portal.ConnectedPortal.ConnectedPortal = createdPortal;
				createdPortal.GetComponentInChildren<SpriteRenderer>().color = 
					portal.ConnectedPortal.GetComponentInChildren<SpriteRenderer>().color;
			}
		}
		createdPortal.PortalIndex = _createdPortalIndex;
	}
}
