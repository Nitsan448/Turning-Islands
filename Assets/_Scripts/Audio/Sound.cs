using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;

    [Range(0,1)]
    public float Volume = 0.7f;

    [HideInInspector] public AudioSource AudioSource;
}
