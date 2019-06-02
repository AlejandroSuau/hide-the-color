using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompletedScreen : MonoBehaviour
{
    public Text eggsDestroyedText, levelIndicator;
    public Button backToLevelsButton, loadLevelButton, nextLevelButton;

    public GameObject[] medals;
    private Color WHITE_COLOR = new Color(255, 255, 255);

    void Start()
    {
        // Unlock new lv if needed.
        if (PlayerPrefs.GetInt("CurrentLevel") == GamePreservedStats.instance.idLevel 
                && GamePreservedStats.instance.idLevel != 6) {
            int unlockedNewLevel = GamePreservedStats.instance.idLevel + 1;
            PlayerPrefs.SetInt("CurrentLevel", unlockedNewLevel);
        }

        if (MenuGameMusicManager.instance != null) {
            StartCoroutine(AudioController.FadeIn(MenuGameMusicManager.instance.audioSource, 1f));
        }

        eggsDestroyedText.text = GamePreservedStats.instance.eggs.ToString();
        
        ShowObtainedMedals();

        string levelToLoad = "Level-" + GamePreservedStats.instance.idLevel;
        levelIndicator.text += GamePreservedStats.instance.idLevel.ToString();

        backToLevelsButton.onClick.AddListener(BackToLevels);
        loadLevelButton.onClick.AddListener(() => LoadLevel(levelToLoad));

        if (GamePreservedStats.instance.idLevel == 6)
        {
            // There is no more levels at the moment. Hide next level button and move the other two.
            nextLevelButton.gameObject.SetActive(false);
            MoveButtons();
        } else
        {
            nextLevelButton.onClick.AddListener(LoadNextLevel);   
        }
    }

    void ShowObtainedMedals()
    {
        for(int i = 0; i < GamePreservedStats.instance.medals; i++) {
            medals[i].GetComponent<SpriteRenderer>().color = WHITE_COLOR;
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level-" + (GamePreservedStats.instance.idLevel+1));
    }

    void BackToLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void MoveButtons()
    {
        float quantityToMove = 0.65f;
        // Move back to levels button 
        Vector3 pos = backToLevelsButton.transform.position;
        pos.x += quantityToMove;
        backToLevelsButton.transform.position = pos;

        // Move load levels button
        pos = loadLevelButton.transform.position;
        pos.x += quantityToMove;
        loadLevelButton.transform.position = pos;
    }
}
