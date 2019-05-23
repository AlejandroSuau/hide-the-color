using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreservedStats : MonoBehaviour
{
    public static GamePreservedStats instance = null;

    public bool gameSuccess;
    public float diedTime;
    public int eggs;
    public int medals;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ResetStats()
    {
        gameSuccess = false;
        eggs = 0;
        diedTime = -1f;
        medals = 0;
    }
}
