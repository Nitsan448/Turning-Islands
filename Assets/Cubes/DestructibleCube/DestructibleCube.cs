using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DestructibleCube : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy;
    [SerializeField] private Sprite[] _sprites;
    private int _currentSpriteIndex = 0;
    [SerializeField] private GameObject _destructionParticlesPrefab;

    public void TakeDamage()
    {
        hitsToDestroy--;
        if (hitsToDestroy == 0)
        {
            Instantiate(_destructionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        _currentSpriteIndex++;
        GetComponent<SpriteRenderer>().sprite = _sprites[_currentSpriteIndex];
    }
}