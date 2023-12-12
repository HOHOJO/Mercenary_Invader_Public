using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int coin;
    public string grade;
    public float timeRemaining = 600.0f;

    private GradeCalculator gradeCalculator;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gradeCalculator = GetComponent<GradeCalculator>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(false);
            }
        }
    }

    public void GameOver(bool isClear)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gradeCalculator != null)
        {
            CalculateAndApplyRewards(isClear);
        }

        UIStageResult stageResultUI = UIManager.Instance.OpenUI<UIStageResult>(0);
        if (isClear)
        {
            stageResultUI.OnStageClear();
            string currentSceneName = SceneManager.GetActiveScene().name;
            string nextSceneName = "Stage" + (int.Parse(currentSceneName.Substring(5)) + 1).ToString();


            if (currentSceneName != "Stage03")
            {
                StageUnlockManager.Instance.UnlockLevel(GetLevelIndex(nextSceneName));
            }
            else
            {
                Invoke("LoadEndingScene", 5f);

            }
        }
        else
        {
            stageResultUI.OnStageFail();
        }
    }

    private void CalculateAndApplyRewards(bool isClear)
    {
        int timeScore = Mathf.RoundToInt(timeRemaining);
        score = timeScore;

        if (gradeCalculator != null)
        {
            grade = gradeCalculator.CalculateGrade(score);
            int reward = gradeCalculator.CalculateRewards(grade);

            if (isClear)
            {
                coin = reward;
                TestCoinManager.Instance.AddCoin(coin);
            }
            else
            {
                int failReward = 10;
                coin = failReward;
            }
        }
    }

    private int GetLevelIndex(string levelName)
    {
        int levelIndex = int.Parse(levelName.Substring(5)) - 1;
        return levelIndex;
    }

    private void LoadEndingScene()
    {
        SceneManager.LoadScene("Ending");
    }
}