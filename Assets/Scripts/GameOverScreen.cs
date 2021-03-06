﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text eggsDestroyedText, levelIndicator;
    public Button backToLevelsButton, loadLevelButton;

    public GameObject[] medals;
    private Color WHITE_COLOR = new Color(255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        if (MenuGameMusicManager.instance != null) {
            StartCoroutine(AudioController.FadeIn(MenuGameMusicManager.instance.audioSource, 1f));
        }

        eggsDestroyedText.text = GamePreservedStats.instance.eggs.ToString();

        string levelToLoad = "Level-" + GamePreservedStats.instance.idLevel;
        levelIndicator.text += GamePreservedStats.instance.idLevel.ToString();

        backToLevelsButton.onClick.AddListener(BackToLevels);
        loadLevelButton.onClick.AddListener(() => LoadLevel(levelToLoad));
    }

    void ShowObtainedMedals()
    {
        for(int i = 0; i < GamePreservedStats.instance.medals; i++) {
            medals[i].GetComponent<SpriteRenderer>().color = WHITE_COLOR;
        }
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
