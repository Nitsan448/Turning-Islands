using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Portal : CubeFace
{
    public Portal ConnectedPortal;
    public bool IsOpen = true;
    public int PortalIndex = -1;
    private float portalDisableTime = 0.5f;
    public PortalGraphics PortalGraphics;
    [FormerlySerializedAs("_collider")] public BoxCollider2D Collider;
    protected override string SoundName { get; set; } = "Portal";

    private void Awake()
    {
        PortalGraphics = GetComponentInChildren<PortalGraphics>();
        PortalGraphics.Light.enabled = IsOpen;
        UpdatePortalColors();
    }

    public void ChangeOpenState(bool changeConnected)
    {
        IsOpen = !IsOpen;
        PortalGraphics.Light.enabled = IsOpen;
        if (ConnectedPortal != null && changeConnected)
        {
            if (ConnectedPortal.IsOpen != IsOpen)
            {
                ConnectedPortal.ChangeOpenState(false);
            }
        }

        PortalGraphics.OpenParticles.gameObject.SetActive(IsOpen);
        MaterialPropertyBlock materialPropertyBlock = new();
        materialPropertyBlock.SetInt("_IsOpen", IsOpen ? 1 : 0);
        materialPropertyBlock.SetColor("_BaseColor", PortalColors.ColorByIndex[PortalIndex]);
        materialPropertyBlock.SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * 4);
        PortalGraphics.Sprite.SetPropertyBlock(materialPropertyBlock);
    }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        if (IsOpen)
        {
            Instantiate(PortalGraphics.EnterParticlesPrefab, PortalGraphics.transform);
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
        ConnectedPortal.Collider.enabled = false;
        Vector3 newPosition = ConnectedPortal.transform.position;
        ball.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        ball.ChangeVelocity(ConnectedPortal.GetVelocity());
        UpdatePortalColors(5.5f);

        float currentTime = 0;
        while (currentTime < portalDisableTime)
        {
            if (currentTime / portalDisableTime < 0.3f)
            {
                UpdatePortalColors(Mathf.Lerp(4, 7, currentTime / portalDisableTime));
            }
            else
            {
                UpdatePortalColors(Mathf.Lerp(7, 4, currentTime / portalDisableTime));
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        UpdatePortalColors();
        ConnectedPortal.Collider.enabled = true;
    }

    public void UpdatePortalColors(float glowIntensity = 4)
    {
        if (PortalIndex == -1)
        {
            return;
        }

        MaterialPropertyBlock materialPropertyBlock = new();
        materialPropertyBlock.SetInt("_IsOpen", IsOpen ? 1 : 0);
        materialPropertyBlock.SetColor("_BaseColor", PortalColors.ColorByIndex[PortalIndex]);
        materialPropertyBlock.SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * glowIntensity);

        PortalGraphics.Light.color = PortalColors.ColorByIndex[PortalIndex];
        PortalGraphics.Sprite.SetPropertyBlock(materialPropertyBlock);
    }
}