using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTutorialButton : MonoBehaviour
{
    private static bool TutorialShown = false;

    // Start is called before the first frame update
    void Start()
    {
        if (TutorialShown)
        {
            Destroy(transform.parent.parent.gameObject);
        }

        Managers.Game.SetStateToTutorial();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Managers.Game.SetStateToEditing();
        transform.parent.parent.gameObject.SetActive(false);
    }
}