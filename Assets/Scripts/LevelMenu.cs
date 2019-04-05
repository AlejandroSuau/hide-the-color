using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button backToMainMenuButton;
    public Button[] levelButtons;

    void Start()
    {
        backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        foreach(Button levelButton in levelButtons) {
            levelButton.onClick.AddListener(() => LoadLevel(levelButton.name));
        }
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void LoadLevel(string buttonName )
    {
        SceneManager.LoadScene(buttonName, LoadSceneMode.Single);
    }
}
