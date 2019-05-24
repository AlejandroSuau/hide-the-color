using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCountdownBehaviour : MonoBehaviour
{
    public Canvas backgroundCanvas;
    public GameObject[] numbers;

    public AudioClip audioPassedSecond;
    public AudioClip audioStartsGame;
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayNumberThreeAnimation", 0.0f);
        Invoke("PlayNumberTwoAnimation", 1.0f);
        Invoke("PlayNumberOneAnimation", 2.0f);
        Invoke("PlayAudioStartsGame", 3.0f);
        Invoke("DestroyStartingCountdown", 4.0f);
    }

    void PlayNumberThreeAnimation()
    {
        audioSource.PlayOneShot(audioPassedSecond, 0.1f);
        numbers[2].SetActive(true);
        numbers[2].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void PlayNumberTwoAnimation()
    {
        audioSource.PlayOneShot(audioPassedSecond, 0.1f);
        numbers[1].SetActive(true);
        numbers[1].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void PlayNumberOneAnimation()
    {
        audioSource.PlayOneShot(audioPassedSecond, 0.1f);
        numbers[0].SetActive(true);
        numbers[0].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void PlayAudioStartsGame()
    {
        audioSource.PlayOneShot(audioStartsGame, 0.5f);
        backgroundCanvas.gameObject.SetActive(false);
    }

    void DestroyStartingCountdown()
    {
        Destroy(gameObject);
    }
}
