using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Game.SetStateToTutorial();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Managers.Game.SetStateToEditing();
        transform.parent.parent.gameObject.SetActive(false);
    }
}