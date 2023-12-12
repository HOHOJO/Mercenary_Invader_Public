using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] Transform levelContainer;
    [SerializeField] TMP_Text levelText;


    List<GameObject> elements = new List<GameObject>();

    GameObject CurrentElement => elements[currentIndex];
    int currentIndex = 0;

    public LevelInfo[] levelInfos;

    public static LevelSelecter Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        for (int i = 0; i < levelContainer.childCount; i++)
        {
            elements.Add(levelContainer.GetChild(i).gameObject);
        }


        if (elements.Count > 0)
        {
            currentIndex = 0;
            CurrentElement.SetActive(true);
            UpdateLevelText(); 
        }

    }
    private void OnEnable()
    {
        LevelSelectEventSystem.OnSelectNextLevel += SelectNextLevel;
        LevelSelectEventSystem.OnSelectPreviousLevel += SelectPreviousLevel;
    }

    private void OnDisable()
    {
        LevelSelectEventSystem.OnSelectNextLevel -= SelectNextLevel;
        LevelSelectEventSystem.OnSelectPreviousLevel -= SelectPreviousLevel;
    }

    private void SelectNextLevel()
    {
        CurrentElement.SetActive(false);
        currentIndex = (currentIndex + 1) % elements.Count;

        CurrentElement.SetActive(true);
        UpdateLevelText();
    }

    private void SelectPreviousLevel()
    {
        CurrentElement.SetActive(false);
        currentIndex = (currentIndex - 1 + elements.Count) % elements.Count;

        CurrentElement.SetActive(true);
        UpdateLevelText();
    }
    private void UpdateLevelText()
    {
        if (levelText != null && currentIndex >= 0 && currentIndex < elements.Count)
        {
            if (currentIndex < levelInfos.Length)
            {
                levelText.text = levelInfos[currentIndex].GetLevelName();
            }
        }
    }
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < levelInfos.Length && DataManager.Instance.nowPlayer.levelUnlocked[levelIndex])
        {
            string levelName = "Stage0" + (levelIndex + 1);
            ScenesManager.Scene scene = (ScenesManager.Scene)Enum.Parse(typeof(ScenesManager.Scene), levelName);
            ScenesManager.Instance.LoadScene(scene);
        }
    }

}
