using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    [SerializeField] private GameObject loaderUI;
    [SerializeField] private Slider progressSlider;

    [SerializeField] private PlayableAsset fadeOutTimeline;

    private PlayableDirector playableDirector;

    private void Awake()
    {
        Instance = this;
        playableDirector = GetComponent<PlayableDirector>();
    }

    public enum Scene
    {
        MainMenu,
        Lobby,
        Stage01,
        Stage02,
        Stage03,
        Ending
    }

    public void LoadScene(Scene scene)
    {
        string sceneName = scene.ToString();
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void LoadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(currentSceneName));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Time.timeScale = 1;
        progressSlider.value = 0;
        loaderUI.SetActive(true);

        yield return FadeSceneOutCoroutine();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;

            if (progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    IEnumerator FadeSceneOutCoroutine()
    {
        playableDirector.Play(fadeOutTimeline);

        yield return new WaitForSeconds((float)playableDirector.duration);
    }
}
