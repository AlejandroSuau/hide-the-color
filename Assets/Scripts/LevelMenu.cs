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

    int currentPlayerLevel;
    private Color WHITE_COLOR = new Color(255, 255, 255);

    void Awake()
    {
        levelsMenu.SetActive(true);
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel")) {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }

        currentPlayerLevel = PlayerPrefs.GetInt("CurrentLevel"); 

        Time.timeScale = 1f;

        if (MenuGameMusicManager.instance != null && !MenuGameMusicManager.instance.audioSource.isPlaying) {
            StartCoroutine(AudioController.FadeIn(MenuGameMusicManager.instance.audioSource, 1f));
        }

        if (GameMusicManager.instance != null && GameMusicManager.instance.audioSource.isPlaying) {
            StartCoroutine(AudioController.FadeOut(GameMusicManager.instance.audioSource, 0.2f));
        }

        backToMainMenuButton.onClick.AddListener(BackToMainMenu);

        for (int i = 0; i < currentPlayerLevel; i++) {
            levelButtons[i].GetComponent<Image>().color = WHITE_COLOR;
            levelButtons[i].interactable = true;
            if (i == 0) 
            {
                levelButtons[i].onClick.AddListener(GoToStory);
            } else
            {
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelButtons[i].name));
            }
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
