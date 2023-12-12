using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestCoinManager : MonoBehaviour
{
    public TMP_Text[] coinTexts;

    public static TestCoinManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin(int amount)
    {
        DataManager.Instance.nowPlayer.coin += amount;
        UpdateCoinText();
        DataManager.Instance.SaveData();
    }

    private void UpdateCoinText()
    {
        string coinValue = DataManager.Instance.nowPlayer.coin.ToString();

        foreach (var coinText in coinTexts)
        {
            coinText.text = coinValue;
        }
    }
}
