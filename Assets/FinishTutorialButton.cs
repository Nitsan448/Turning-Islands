using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishTutorialButton : MonoBehaviour
{
    [SerializeField] private string _tutorialName;

    void Start()
    {
        if (PlayerPrefs.GetInt(_tutorialName, 0) == 1)
        {
            Destroy(transform.parent.parent.gameObject);
            return;
        }

        Managers.Game.SetStateToTutorial();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Managers.Game.SetStateToEditing();
        transform.parent.parent.gameObject.SetActive(false);
        PlayerPrefs.SetInt(_tutorialName, 1);
    }
}