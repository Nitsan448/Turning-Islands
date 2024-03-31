using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public enum ETimePeriod
{
    Day,
    Sunset,
    Night,
}
#if UNITY_EDITOR

public class TimePeriodSetter : MonoBehaviour
{
    [SerializeField] private ETimePeriod _timePeriod;

    [Button]
    private void SetTimePeriod()
    {
        GameObject background = GameObject.Find("Background");
        GameObject light = GameObject.Find("Light");
        if (background != null)
        {
            DestroyImmediate(background);
        }

        if (light != null)
        {
            DestroyImmediate(light);
        }

        string prefabsPath = "Assets/Prefabs/SceneBuilder/";
        if (_timePeriod == ETimePeriod.Night)
        {
            prefabsPath += "Night/";
        }
        else if (_timePeriod == ETimePeriod.Sunset)
        {
            prefabsPath += "Sunset/";
        }
        else
        {
            prefabsPath += "Day/";
        }

        background = AssetDatabase.LoadAssetAtPath<GameObject>(prefabsPath + "Background.prefab");
        light = AssetDatabase.LoadAssetAtPath<GameObject>(prefabsPath + "light.prefab");

        background = PrefabUtility.InstantiatePrefab(background) as GameObject;
        light = PrefabUtility.InstantiatePrefab(light) as GameObject;
    }
}
#endif