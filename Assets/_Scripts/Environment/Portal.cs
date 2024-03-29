using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Portal : CubeFace
{
    public Portal ConnectedPortal;
    public bool IsOpen = true;
    public int PortalIndex = -1;
    private float portalDisableTime = 0.5f;
    public SpriteRenderer _portalSprite;
    public BoxCollider2D _collider;
    protected override string SoundName { get; set; } = "Portal";

    private void Awake()
    {
        Light2D portalLight = GetComponentInChildren<Light2D>();
        portalLight.enabled = IsOpen;
        UpdatePortalColors();
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

        MaterialPropertyBlock materialPropertyBlock = new();
        materialPropertyBlock.SetInt("_IsOpen", IsOpen ? 1 : 0);
        materialPropertyBlock.SetColor("_BaseColor", PortalColors.ColorByIndex[PortalIndex]);
        materialPropertyBlock.SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * 4);
        _portalSprite.SetPropertyBlock(materialPropertyBlock);
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
        ConnectedPortal._collider.enabled = false;
        Vector3 newPosition = ConnectedPortal.transform.position;
        ball.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        ball.ChangeVelocity(ConnectedPortal.GetVelocity());

        yield return new WaitForSeconds(portalDisableTime);
        ConnectedPortal._collider.enabled = true;
    }

    public void UpdatePortalColors()
    {
        MaterialPropertyBlock materialPropertyBlock = new();
        materialPropertyBlock.SetInt("_IsOpen", IsOpen ? 1 : 0);
        materialPropertyBlock.SetColor("_BaseColor", PortalColors.ColorByIndex[PortalIndex]);
        materialPropertyBlock.SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * 4);
        _portalSprite.GetComponent<Light2D>().color = PortalColors.ColorByIndex[PortalIndex];
        _portalSprite.SetPropertyBlock(materialPropertyBlock);
    }
}