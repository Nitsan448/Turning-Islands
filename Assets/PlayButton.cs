using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { Managers.GameManager.StartGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
