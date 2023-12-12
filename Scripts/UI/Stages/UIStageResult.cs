using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStageResult : MonoBehaviour
{
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnQuitGame;

    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private TMP_Text rewardsText;


    private void Start()
    {
        btnRestart.onClick.AddListener(OpenPopup_RestartGame);
        btnQuitGame.onClick.AddListener(OpenPopup_QuitGame);
    }

    void OpenPopup_RestartGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("�絵��", "���������� �ٽ� �����Ͻðڽ��ϱ�?", () => { ScenesManager.Instance.LoadCurrentScene(); });
    }

    void OpenPopup_QuitGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("�������� ����", "�κ�� �̵��Ͻðڽ��ϱ�?", () => { ScenesManager.Instance.LoadScene(ScenesManager.Scene.Lobby); });
    }

    public void OnStageClear()
    {
        resultText.text = "�������� Ŭ����!";
        scoreText.text = "����: " + GameManager.Instance.score.ToString();
        gradeText.text = "���: " + GameManager.Instance.grade.ToString();
        rewardsText.text = "����: " + GameManager.Instance.coin.ToString() + "G";
    }

    public void OnStageFail()
    {
        resultText.text = "�������� ����!";
        scoreText.text = "����: " + GameManager.Instance.score.ToString();
        gradeText.text = "���: F";
        rewardsText.text = "����: " + GameManager.Instance.coin.ToString() + "G";
    }
}
