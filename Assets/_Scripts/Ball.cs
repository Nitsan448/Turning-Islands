using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

	private void Awake()
	{
        _rigidBody = GetComponent<Rigidbody2D>();
	}

	public void ChangeVelocity(Vector2 velocity)
	{
		_rigidBody.velocity = velocity;
	}
}
