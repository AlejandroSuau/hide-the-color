﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverLogo, gameCompletedLogo;
    public Text eggsDestroyedText;
    public Button backToLevelsButton, loadLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        eggsDestroyedText.text = GamePreservedStats.instance.eggs.ToString();

        if (GamePreservedStats.instance.gameSuccess)
            gameCompletedLogo.SetActive(true);
        else
            gameOverLogo.SetActive(true);
        
        string levelToLoad = "Level01";

        backToLevelsButton.onClick.AddListener(BackToLevels);
        loadLevelButton.onClick.AddListener(() => LoadLevel(levelToLoad));
    }

    void BackToLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}