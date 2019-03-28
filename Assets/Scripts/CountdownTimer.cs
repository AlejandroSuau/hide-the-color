using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text textTime;
    public Text textSeconds;
    
    [SerializeField]
    private float startingTime;
    
    private float currentTime;
    private bool isPaused;
    private float secondElapsing;

    void Start()
    {
        currentTime = startingTime;
        secondElapsing = 0;
        UpdateTextTime();
    }

    void Update()
    {
        if (!HasEnded() && !isPaused) {
            currentTime -= Time.deltaTime;
            
            secondElapsing += Time.deltaTime;
            if (secondElapsing >= 1 || currentTime <= 0) {
                UpdateTextTime();
                secondElapsing = 0;
            }
        }
    }

    void UpdateTextTime()
    {
        textSeconds.text = (Mathf.Ceil(currentTime)).ToString();
    }

    bool HasEnded()
    {
        return currentTime <= 0;
    }

    void Reset()
    {
        currentTime = startingTime;
    }
}
