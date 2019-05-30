using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public GameObject storyScreen;
    public Button buttonPlayLevelOne;

    public GameObject loadingScreen;
    public Slider slider;
    
    void Start()
    {
        storyScreen.SetActive(true);
        buttonPlayLevelOne.onClick.AddListener(() => LoadLevel("Level-1"));
    }

    void LoadLevel(string buttonName)
    {
        StartCoroutine(LoadAsynchronously(buttonName));
    }

    IEnumerator LoadAsynchronously(string buttonName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(buttonName);

        storyScreen.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
