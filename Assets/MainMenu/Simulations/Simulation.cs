using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    public float SimulationLength;

    private void Start()
    {
        _ball.StartMoving();
    }
}