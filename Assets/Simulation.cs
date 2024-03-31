using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] protected Ball _ball;
    [SerializeField] private float _simulationLength;

    private void Start()
    {
        _ball.StartMoving();
    }
}