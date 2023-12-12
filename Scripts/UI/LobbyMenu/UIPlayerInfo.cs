using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text HpValue;
    [SerializeField] private TMP_Text AtkValue;
    [SerializeField] private TMP_Text DEFValue;
    [SerializeField] private TMP_Text SpValue;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image atkBar;
    [SerializeField] private Image defBar;
    [SerializeField] private Image speedBar;

    private void Start()
    {
        playerNameText.text = DataManager.Instance.nowPlayer.name.ToString();
        HpValue.text= DataManager.Instance.nowPlayer.hp.ToString();
        AtkValue.text = DataManager.Instance.nowPlayer.attack.ToString();
        DEFValue.text = DataManager.Instance.nowPlayer.def.ToString();
        SpValue.text = DataManager.Instance.nowPlayer.sp.ToString();

        hpBar.fillAmount = DataManager.Instance.nowPlayer.hp / 1000;
        atkBar.fillAmount = DataManager.Instance.nowPlayer.attack / 100;
        defBar.fillAmount = DataManager.Instance.nowPlayer.def / 100;
        speedBar.fillAmount = DataManager.Instance.nowPlayer.sp / 100;
    }
}
