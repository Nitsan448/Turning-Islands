using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCube : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy;

    public void TakeDamage()
    {
        hitsToDestroy--;
        if (hitsToDestroy == 0)
        {
            Destroy(gameObject);
        }
    }
}