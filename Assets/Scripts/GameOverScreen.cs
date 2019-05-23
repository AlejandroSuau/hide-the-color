using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverLogo, gameCompletedLogo;
    public Text eggsDestroyedText;
    public Button backToLevelsButton, loadLevelButton;

    public GameObject[] medals;
    private Color WHITE_COLOR = new Color(255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        eggsDestroyedText.text = GamePreservedStats.instance.eggs.ToString();

        if (GamePreservedStats.instance.gameSuccess) {
            gameCompletedLogo.SetActive(true);
            ShowObtainedMedals();
        } else {
            gameOverLogo.SetActive(true);
        }

        string levelToLoad = "Level01";

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
