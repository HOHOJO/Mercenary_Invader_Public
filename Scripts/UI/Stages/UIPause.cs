using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuitGame;

    [SerializeField] private GameObject btnPause;

    private void Start()
    {
        btnResume.onClick.AddListener(ResumeGame);
        btnRestart.onClick.AddListener(RestartGame);
        btnSettings.onClick.AddListener(OpenPanel_Settings);
        btnQuitGame.onClick.AddListener(OpenPopup_QuitGame);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        var animUI = gameObject.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(gameObject);
        }

        btnPause.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void RestartGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("재시작", "게임을 다시 시작하시겠습니까?", () => { ScenesManager.Instance.LoadCurrentScene(); });
    }

    void OpenPanel_Settings()
    {
        UIManager.Instance.OpenUI<UISettings>(0);
    }

    void OpenPopup_QuitGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("게임 종료", "게임을 종료하시겠습니까?", () => { ScenesManager.Instance.LoadScene(ScenesManager.Scene.Lobby); });
    }
}
