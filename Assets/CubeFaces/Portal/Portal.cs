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
    private float _portalDisableTime = 0.5f;
    public PortalGraphics PortalGraphics;
    [FormerlySerializedAs("_collider")] public BoxCollider2D Collider;
    protected override string SoundName { get; set; } = "Portal";

    [Header("Activation Effect")] [SerializeField]
    private float _activationEffectDuration = 0.7f;

    private readonly float _defaultGlowIntensity = 18;
    private readonly float _activationGlowIntensity = 36;


    private void Awake()
    {
        PortalGraphics = GetComponentInChildren<PortalGraphics>();
        PortalGraphics.OpenParticles.gameObject.SetActive(IsOpen);
        PortalGraphics.Light.enabled = IsOpen;
        PortalGraphics.PortalInside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
        PortalGraphics.PortalOutside.material.SetInt("_IsOpen", IsOpen ? 1 : 0);
        UpdatePortalColors(_defaultGlowIntensity);
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

        yield return new WaitForSeconds(_portalDisableTime);

        ConnectedPortal.Collider.enabled = true;
    }

    private IEnumerator PortalActivatedEffect()
    {
        Instantiate(PortalGraphics.EnterParticlesPrefab, PortalGraphics.transform);
        float currentTime = 0;
        while (currentTime < _activationEffectDuration)
        {
            float t = currentTime / _activationEffectDuration < 0.1f
                ? Mathf.Lerp(_defaultGlowIntensity, _activationGlowIntensity, currentTime / _activationEffectDuration)
                : Mathf.Lerp(_activationGlowIntensity, _defaultGlowIntensity, currentTime / _activationEffectDuration);
            UpdatePortalColors(t);

            currentTime += Time.deltaTime;
            yield return null;
        }

        UpdatePortalColors(_defaultGlowIntensity);
    }

    public void UpdatePortalColors(float glowIntensity)
    {
        PortalGraphics.PortalInside.material
            .SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * glowIntensity);
        PortalGraphics.PortalOutside.material
            .SetColor("_GlowColor", PortalColors.ColorByIndex[PortalIndex] * glowIntensity);

        PortalGraphics.Light.color = PortalColors.ColorByIndex[PortalIndex];
    }
}