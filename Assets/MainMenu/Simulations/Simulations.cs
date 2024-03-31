using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Simulations : MonoBehaviour
{
    [SerializeField] private List<GameObject> _simulationPrefabs;
    [ReadOnly] [SerializeField] private List<GameObject> _simulationsToChooseFrom = new();
    private Simulation _currentSimulation;

    private float _timeSinceLastSimulationStarted;

    private void Update()
    {
        if (_currentSimulation == null || _timeSinceLastSimulationStarted > _currentSimulation.SimulationLength)
        {
            if (_simulationsToChooseFrom.Count == 0)
            {
                PopulateSimulationsToChooseFrom();
            }

            _timeSinceLastSimulationStarted = 0;
            InstantiateRandomSimulation();
        }

        _timeSinceLastSimulationStarted += Time.deltaTime;
    }

    private void PopulateSimulationsToChooseFrom()
    {
        foreach (GameObject simulationPrefab in _simulationPrefabs)
        {
            _simulationsToChooseFrom.Add(simulationPrefab);
        }
    }

    private void InstantiateRandomSimulation()
    {
        if (_currentSimulation != null)
        {
            Destroy(_currentSimulation.gameObject);
        }

        int simulationIndex = Random.Range(0, _simulationsToChooseFrom.Count);
        _currentSimulation = Instantiate(_simulationsToChooseFrom[simulationIndex], transform).GetComponent<Simulation>();
        _simulationsToChooseFrom.RemoveAt(simulationIndex);
    }
}