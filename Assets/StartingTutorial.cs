using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _selectCubesTutorial;
    [SerializeField] private GameObject _rotateCubesTutorial;
    [SerializeField] private GameObject _howToWinTutorial;

    private void Start()
    {
        StartCoroutine(RunTutorial());
    }


    private IEnumerator RunTutorial()
    {
        _selectCubesTutorial.SetActive(true);
        _rotateCubesTutorial.SetActive(false);
        _howToWinTutorial.SetActive(false);
        while (!Managers.Cubes.SelectedCubeChanged)
        {
            yield return null;
        }

        _selectCubesTutorial.SetActive(false);
        _rotateCubesTutorial.SetActive(true);
        while (!Managers.Cubes.CubeRotated)
        {
            yield return null;
        }

        _rotateCubesTutorial.SetActive(false);
        _howToWinTutorial.SetActive(true);
    }
}