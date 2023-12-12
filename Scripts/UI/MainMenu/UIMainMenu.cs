using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button btnStartGame;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnQuitGame;
    [SerializeField] private Button btnTutorial;
    [SerializeField] private Button btnURL;

    [SerializeField] AudioClip clickSound;

    private void Start()
    {
        Action buttonClickAction = () => SoundManager.Instance.SFXPlay("btnClick", clickSound);

        btnStartGame.onClick.AddListener(() => { buttonClickAction(); OpenPanel_StartGame(); });
        btnSettings.onClick.AddListener(() => { buttonClickAction(); OpenPanel_Settings(); });
        btnCredits.onClick.AddListener(() => { buttonClickAction(); OpenPanel_Credits(); });
        btnQuitGame.onClick.AddListener(() => { buttonClickAction(); OpenPopup_QuitGame(); });
        btnTutorial.onClick.AddListener(() => { buttonClickAction(); OpenPanel_Tutorial(); });
        btnURL.onClick.AddListener(() => { buttonClickAction(); OpenURL(); });
    }
    public void OpenPanel_StartGame()
    {
        UIManager.Instance.OpenUI<UISlots>(0);
    }

    void OpenPanel_Settings()
    {
        UIManager.Instance.OpenUI<UISettings>(0);
    }

    void OpenPanel_Credits()
    {
        UIManager.Instance.OpenUI<UICredits>(0);
    }

    void OpenPopup_QuitGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("게임 종료", "게임을 종료하시겠습니까?", () => { ScenesManager.Instance.QuitGame(); });
    }

    public void OpenPanel_Tutorial()
    {
        UIManager.Instance.OpenUI<UITutorial>(0);
    }

    public void OpenURL()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("설문 조사", "설문조사 페이지로 이동하시겠습니까?", () => { Application.OpenURL("https://docs.google.com/forms/d/17mip4sAIKkJhR7Iu-vL9VZTp-Beye9SewNYamcWTg5c/closedform?pli=1"); });
    }
}
