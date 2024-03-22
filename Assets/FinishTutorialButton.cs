using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishTutorialButton : MonoBehaviour
{
    void Start()
    {
        // if ()
        // {
        //     Destroy(transform.parent.parent.gameObject);
        // }

        Managers.Game.SetStateToTutorial();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Managers.Game.SetStateToEditing();
        transform.parent.parent.gameObject.SetActive(false);
    }
}