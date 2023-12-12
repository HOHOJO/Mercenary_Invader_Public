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
        popup.SetPopup("재도전", "스테이지를 다시 도전하시겠습니까?", () => { ScenesManager.Instance.LoadCurrentScene(); });
    }

    void OpenPopup_QuitGame()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("스테이지 종료", "로비로 이동하시겠습니까?", () => { ScenesManager.Instance.LoadScene(ScenesManager.Scene.Lobby); });
    }

    public void OnStageClear()
    {
        resultText.text = "스테이지 클리어!";
        scoreText.text = "점수: " + GameManager.Instance.score.ToString();
        gradeText.text = "등급: " + GameManager.Instance.grade.ToString();
        rewardsText.text = "보상: " + GameManager.Instance.coin.ToString() + "G";
    }

    public void OnStageFail()
    {
        resultText.text = "스테이지 실패!";
        scoreText.text = "점수: " + GameManager.Instance.score.ToString();
        gradeText.text = "등급: F";
        rewardsText.text = "보상: " + GameManager.Instance.coin.ToString() + "G";
    }
}
