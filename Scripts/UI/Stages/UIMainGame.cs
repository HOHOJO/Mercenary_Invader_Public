using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainGame : MonoBehaviour
{
    [SerializeField] private Button btnPause;
    [SerializeField] private TMP_Text timerText;


    private void Start()
    {
        btnPause.onClick.AddListener(OpenPanel_Pause);

    }

    private void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.Instance.timeRemaining);
        timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    void OpenPanel_Pause()
    {
        UIManager.Instance.OpenUI<UIPause>(0);
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
