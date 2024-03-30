using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class PortalGraphics : MonoBehaviour
{
    public ParticleSystem OpenParticles;
    public GameObject EnterParticlesPrefab;
    public SpriteRenderer PortalOutside;
    public SpriteRenderer PortalInside;
    public Light2D Light;
}