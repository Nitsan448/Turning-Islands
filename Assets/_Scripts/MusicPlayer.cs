using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; set; }
    void Start()
    {
        if(Instance == null)
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
		else
		{
            Destroy(gameObject);
		}
    }
}
