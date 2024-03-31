using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Simulations : MonoBehaviour
{
    [SerializeField] private List<GameObject> _simulationPrefabs;
    private List<GameObject> _simulationsToChooseFrom;
    private Simulation _currentSimulation;

    private float _timeSinceLastSimulationStarted;

    private void Start()
    {
        _simulationsToChooseFrom = _simulationPrefabs;
    }

    private void Update()
    {
        if (_currentSimulation == null || _timeSinceLastSimulationStarted > _currentSimulation.SimulationLength)
        {
            if (_simulationsToChooseFrom.Count == 0)
            {
                _simulationsToChooseFrom = _simulationPrefabs;
            }

            _timeSinceLastSimulationStarted = 0;
            InstantiateRandomSimulation();
        }

        _timeSinceLastSimulationStarted += Time.deltaTime;
    }

    private void InstantiateRandomSimulation()
    {
        int simulationIndex = Random.Range(0, _simulationsToChooseFrom.Count);
        _currentSimulation = Instantiate(_simulationsToChooseFrom[simulationIndex]).GetComponent<Simulation>();
        _simulationsToChooseFrom.RemoveAt(simulationIndex);
    }
}