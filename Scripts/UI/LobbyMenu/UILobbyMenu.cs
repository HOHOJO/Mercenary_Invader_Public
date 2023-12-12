using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyMenu : MonoBehaviour
{
    [SerializeField] private Button btnStageSelect;
    //[SerializeField] private Button btnUserInfo;
    //[SerializeField] private Button btnShop;
    [SerializeField] private Button btnToMainMenu;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnBack;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [SerializeField] private Button[] stageButtons;

    [SerializeField] private Sprite lockedImage;

    private void Start()
    {
        btnStageSelect.onClick.AddListener(OpenPanel_StageSelect);
        btnSettings.onClick.AddListener(OpenPanel_Settings);
        //btnUserInfo.onClick.AddListener(OpenPanel_PlayerInfo);
        //btnShop.onClick.AddListener(OpenPanel_Shop);
        btnToMainMenu.onClick.AddListener(OpenScene_MainMenu);
        btnBack.onClick.AddListener(OpenPanel_LobbyMenu);

        nextButton.onClick.AddListener(() => LevelSelectEventSystem.SelectNextLevel());
        prevButton.onClick.AddListener(() => LevelSelectEventSystem.SelectPreviousLevel());

        for (int i = 0; i < stageButtons.Length; i++)
        {
            int index = i;
            stageButtons[i].onClick.AddListener(() => LoadLevel(index));
        }
    }

    void OpenPanel_StageSelect()
    {
        gameObject.SetActive(false);
    }

    void OpenPanel_LobbyMenu()
    {
        gameObject.SetActive(true);
    }

    void OpenPanel_Settings()
    {
        UIManager.Instance.OpenUI<UISettings>(0);
    }

    void OpenPanel_PlayerInfo()
    {
        UIManager.Instance.OpenUI<UIPlayerInfo>(0);
    }

    void OpenPanel_Shop()
    {
        UIManager.Instance.OpenUI<UIShop>(0);
    }

    void OpenScene_MainMenu()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("메인 메뉴", "메인 메뉴로 돌아가시겠습니까?", () => { ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu); });
    }

    void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < stageButtons.Length)
        {
            if (levelIndex < LevelSelecter.Instance.levelInfos.Length && DataManager.Instance.nowPlayer.levelUnlocked[levelIndex])
            {
                LevelSelecter.Instance.LoadLevel(levelIndex);
            }
            else
            {
                stageButtons[levelIndex].GetComponent<Button>().interactable = false;
            }
        }
    }
}
