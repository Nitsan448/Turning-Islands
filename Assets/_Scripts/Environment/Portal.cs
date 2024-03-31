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
    private float portalActivatedEffectDuration = 0.7f;
    public PortalGraphics PortalGraphics;
    [FormerlySerializedAs("_collider")] public BoxCollider2D Collider;
    protected override string SoundName { get; set; } = "Portal";

    private void Awake()
    {
        Debug.Log(gameObject.name + ": " + IsOpen);
        PortalGraphics = GetComponentInChildren<PortalGraphics>();
        PortalGraphics.OpenParticles.gameObject.SetActive(IsOpen);
        PortalGraphics.Light.enabled = IsOpen;
        PortalGraphics.PortalInside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
        PortalGraphics.PortalOutside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
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
        if (Application.isPlaying)
        {
            PortalGraphics.PortalInside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
            PortalGraphics.PortalOutside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
        }
    }

    protected override void OnCollisionOrTrigger(Ball ball)
    {
        if (IsOpen)
        {
            StartCoroutine(PortalActivatedEffect());
            StartCoroutine(ConnectedPortal.PortalActivatedEffect());
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

        yield return new WaitForSeconds(portalDisableTime);

        ConnectedPortal.Collider.enabled = true;
    }

    private IEnumerator PortalActivatedEffect()
    {
        Instantiate(PortalGraphics.EnterParticlesPrefab, PortalGraphics.transform);
        float currentTime = 0;
        while (currentTime < portalActivatedEffectDuration)
        {
            float t = currentTime / portalActivatedEffectDuration < 0.2f
                ? Mathf.Lerp(18, 30, currentTime / portalActivatedEffectDuration)
                : Mathf.Lerp(30, 18, currentTime / portalActivatedEffectDuration);
            UpdatePortalColors(t);

            currentTime += Time.deltaTime;
            yield return null;
        }

        UpdatePortalColors();
    }

    public void UpdatePortalColors(float glowIntensity = 18)
    {
        PortalGraphics.PortalInside.material
            .SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * glowIntensity);
        PortalGraphics.PortalOutside.material
            .SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * glowIntensity);

        PortalGraphics.Light.color = PortalColors.ColorByIndex[PortalIndex];
    }
}