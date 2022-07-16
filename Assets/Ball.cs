using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    //private 

	private void Awake()
	{
        _rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		Managers.GameManager.Ball = this;
	}

	public void ChangeVelocity(Vector2 velocity)
	{
		_rigidBody.velocity = velocity;
	}
}
