using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class LevelSelectGrid : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;

#if UNITY_EDITOR
    [Button("Initialize Grid And Delete Existing")]
    private void InitializeGridAndOverrideExisting()
    {
        Rect rect = _buttonPrefab.GetComponent<RectTransform>().rect;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(rect.width, rect.height);

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int levelIndex = 1; levelIndex < SceneManager.sceneCountInBuildSettings; levelIndex++)
        {
            //TODO: go over all scenes and add ones who's name starts with level 
            AddButton(levelIndex);
        }
    }
#endif

    [Button("Add missing levels")]
    private void AddMissingLevels()
    {
        for (int levelIndex = transform.childCount + 1;
             levelIndex < SceneManager.sceneCountInBuildSettings - 1;
             levelIndex++)
        {
            AddButton(levelIndex);
        }
    }

    private void AddButton(int levelIndex)
    {
        GameObject levelSelectButton = Instantiate(_buttonPrefab, transform);
        levelSelectButton.name = "Level " + levelIndex;
        levelSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = levelIndex.ToString();
    }

    private static void StartLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    private void Start()
    {
        AddMissingLevels();

        Button[] buttons = GetComponentsInChildren<Button>();
        for (int index = 0; index < buttons.Length; index++)
        {
            int index1 = index;
            buttons[index].onClick.AddListener(delegate { StartLevel(index1 + 1); });
        }
    }
}