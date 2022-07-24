using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private Sound[] _sounds;
    // Start is called before the first frame update
    public void Startup()
    {
        foreach(Sound sound in _sounds)
		{
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.AudioClip;
            sound.AudioSource.volume = sound.Volume;
		}
    }

    public void PlaySound(string soundName)
	{
        Sound sound = Array.Find(_sounds, sound => sound.Name == soundName);
        if(sound != null)
		{
            sound.AudioSource.Play();
		}
		else
		{
            Debug.LogWarning("Could not find sound: " + soundName);
		}
	}
}
