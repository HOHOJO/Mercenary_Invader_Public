using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUnlockManager : MonoBehaviour
{
    public static StageUnlockManager Instance;

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
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < DataManager.Instance.nowPlayer.levelUnlocked.Count)
        {
            DataManager.Instance.nowPlayer.levelUnlocked[levelIndex] = true;
            
            DataManager.Instance.SaveData();
        }
    }
}
