using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenButtons : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public Button restartLevelButton, pauseButton, playButton, backToLevelsButton;

    // Start is called before the first frame update
    void Start()
    {
        restartLevelButton.onClick.AddListener(RestartLevel);
        pauseButton.onClick.AddListener(Pause);
        playButton.onClick.AddListener(Play);
        backToLevelsButton.onClick.AddListener(BackToLevelsMenu);
    }

    void BackToLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    void Play()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
