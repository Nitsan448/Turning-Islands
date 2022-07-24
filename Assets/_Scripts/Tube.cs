using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : CubeFace
{
	public Tube ConnectedTube;
	public bool Turned;
	public float TimeInsideTube = 0.3f;
	private float _tubeDisableTime = 0.7f;
	protected override string SoundName { get; set; } = "No sound";

	protected override void OnCollisionOrTrigger(Ball ball)
	{
		StartCoroutine(SwitchTube(ball));
	}

	private IEnumerator SwitchTube(Ball ball)
	{
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(TimeInsideTube);

		ConnectedTube.GetComponent<BoxCollider2D>().enabled = false;
		Vector3 newPosition = ConnectedTube.transform.position;
		ball.transform.position = new Vector3(newPosition.x, newPosition.y + 0.1f, newPosition.z);
		ball.ChangeVelocity(ConnectedTube.GetComponent<CubeFace>().GetVelocity());
		ball.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(_tubeDisableTime);

		ConnectedTube.GetComponent<BoxCollider2D>().enabled = true;
	}
}
