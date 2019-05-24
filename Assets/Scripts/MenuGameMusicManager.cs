using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameMusicManager : MonoBehaviour
{
    public static MenuGameMusicManager instance = null;

    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        audioSource = GetComponent<AudioSource>();
    }
}
