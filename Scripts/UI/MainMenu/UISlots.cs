using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UISlots : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private GameObject createPlayerPanel;
    [SerializeField] private TMP_Text newPlayerName;

    [SerializeField] private GameObject[] newPlayerImg;
    [SerializeField] private TMP_Text[] slotText;
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private Button[] deleteSlotButtons;

    [SerializeField] AudioClip clickSound;

    private bool[] saveFile = new bool[3];

    private void Start()
    {
        Action buttonClickAction = () => SoundManager.Instance.SFXPlay("btnClick", clickSound);

        SetSlotsData();

        btnBack.onClick.AddListener(() => { buttonClickAction(); Back_Slots(); });

        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;
            slotButtons[i].onClick.AddListener(() => { buttonClickAction(); SelectSlot(index); });
            deleteSlotButtons[i].onClick.AddListener(() => { buttonClickAction(); OpenPopup_DeleteSlot(index); });
        }
    }

    private void SetSlotsData()
    {
        for (int i = 0; i < saveFile.Length; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))
            {
                saveFile[i] = true;
                DataManager.Instance.nowSlot = i;
                DataManager.Instance.LoadData();
                slotText[i].text = $"<b>{DataManager.Instance.nowPlayer.name}</b>\n\n[{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}]";
                newPlayerImg[i].SetActive(true);
            }
            else
            {
                slotText[i].text = "ºó ½½·Ô";
                newPlayerImg[i].SetActive(false);
            }
        }
        DataManager.Instance.DataClear();
    }

    void SelectSlot(int num)
    {
        DataManager.Instance.nowSlot = num;

        if (saveFile[num])
        {
            DataManager.Instance.LoadData();
            EnterLobby();
        }
        else
        {
            OpenPanel_CreatePlayer();
        }
    }

    public void EnterLobby()
    {
        if (!saveFile[DataManager.Instance.nowSlot])
        {
            DataManager.Instance.nowPlayer.name = newPlayerName.text;
            DataManager.Instance.SaveData();
        }

        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Lobby);
    }

    private void OpenPanel_CreatePlayer()
    {
        UIManager.Instance.OpenUI<UICreatePlayer>(0);
    }

    private void DeleteSlot(int num)
    {
        DataManager.Instance.nowSlot = num;
        DataManager.Instance.DeleteData();
        slotText[num].text = "ºó ½½·Ô";
        saveFile[num] = false;
        newPlayerImg[num].SetActive(false);
    }

    private void OpenPopup_DeleteSlot(int num)
    {
        if (saveFile[num])
        {
            UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
            popup.SetPopup("µ¥ÀÌÅÍ »èÁ¦", "ÀúÀåµÈ ½½·ÔÀ» »èÁ¦ÇÏ½Ã°Ú½À´Ï±î?", () => { DeleteSlot(num); });
        }
    }

    void Back_Slots()
    {
        var animUI = gameObject.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(gameObject);
        }
    }
}
