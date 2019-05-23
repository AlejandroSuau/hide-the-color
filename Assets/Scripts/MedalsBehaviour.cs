using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalsBehaviour : MonoBehaviour
{
    public GameObject[] medalsGO; // Third medal => medalsGO[0]
    public GameObject[] medalsObtainedGO;
    public int[] medalsNecessaryEggs; // Third medal necessary eggs -> medalsNecessaryEggs[0]

    private int currentIndexMedalToObtain;

    AudioSource audioSource;
    public AudioClip audioMedalObtained;

    private Color WHITE_COLOR = new Color(255, 255, 255);

    public int ObtainedMedals { get { return currentIndexMedalToObtain; } }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentIndexMedalToObtain = 0;
    }

    public bool DoesItObtainsANewMedalWithThis(int eggs)
    {
        if (currentIndexMedalToObtain == medalsGO.Length || eggs < medalsNecessaryEggs[currentIndexMedalToObtain]) {
            return false;
        } else {
            // Obtain new medal
            medalsGO[currentIndexMedalToObtain].GetComponent<SpriteRenderer>().color = WHITE_COLOR;
            medalsObtainedGO[currentIndexMedalToObtain].SetActive(true);

            audioSource.PlayOneShot(audioMedalObtained, 0.5f);
            currentIndexMedalToObtain ++;

            return true;
        }
    }
}
