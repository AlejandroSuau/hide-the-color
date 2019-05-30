using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelsMenu;
    public Button backToMainMenuButton;
    public Button[] levelButtons;

    public GameObject loadingScreen;
    public Slider slider;

    void Awake()
    {
        levelsMenu.SetActive(true);
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (MenuGameMusicManager.instance != null && !MenuGameMusicManager.instance.audioSource.isPlaying) {
            StartCoroutine(AudioController.FadeIn(MenuGameMusicManager.instance.audioSource, 1f));
        }

        if (GameMusicManager.instance != null && GameMusicManager.instance.audioSource.isPlaying) {
            StartCoroutine(AudioController.FadeOut(GameMusicManager.instance.audioSource, 0.2f));
        }

        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        int i = 0;
        foreach(Button levelButton in levelButtons) {
            if (i == 0) {
                levelButton.onClick.AddListener(GoToStory);
            } else {
                levelButton.onClick.AddListener(() => LoadLevel(levelButton.name));
            }
            i ++;
        }
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void GoToStory()
    {
        SceneManager.LoadScene("Story", LoadSceneMode.Single);
    }

    void LoadLevel(string buttonName)
    {
        StartCoroutine(LoadAsynchronously(buttonName));
    }

    IEnumerator LoadAsynchronously(string buttonName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(buttonName);
        levelsMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
