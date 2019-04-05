using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenButtons : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public Button restartLevelButton, pauseButton, playButton, backToLevelsButton;

    // Start is called before the first frame update
    void Start()
    {
        //restartLevelButton.onClick.AddListener();
        pauseButton.onClick.AddListener(Pause);
        playButton.onClick.AddListener(Play);
        //backToLevelsButton.onClick.AddListener();
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
}
