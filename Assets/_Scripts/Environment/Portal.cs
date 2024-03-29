using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : CubeFace
{
    public Portal ConnectedPortal;
    public bool IsOpen = true;
    public int PortalIndex = -1;
    private float portalDisableTime = 0.5f;
    private GameObject _portalSprite;
    private BoxCollider2D _collider;
    protected override string SoundName { get; set; } = "Portal";

    private void Awake()
    {
        _portalSprite = GetComponentInChildren<Light2D>().gameObject;
        _portalSprite.GetComponent<Light2D>().enabled = IsOpen;
    }

    public void ChangeOpenState(bool changeConnected)
    {
        IsOpen = !IsOpen;
        _portalSprite.GetComponent<Light2D>().enabled = IsOpen;
        if (ConnectedPortal != null && changeConnected)
        {
            if (ConnectedPortal.IsOpen != IsOpen)
            {
                ConnectedPortal.ChangeOpenState(false);
            }
        }
    }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        if (IsOpen)
        {
            StartCoroutine(SwitchPortals(ball));
        }
        else
        {
            ball.ChangeVelocity(GetVelocity());
            ball.Animator.Play("Squish");
        }
    }

    private IEnumerator SwitchPortals(Ball ball)
    {
        ConnectedPortal.GetComponent<BoxCollider2D>().enabled = false;
        Vector3 newPosition = ConnectedPortal.transform.position;
        ball.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        ball.ChangeVelocity(ConnectedPortal.GetVelocity());

        yield return new WaitForSeconds(portalDisableTime);
        ConnectedPortal.GetComponent<BoxCollider2D>().enabled = true;
    }
}