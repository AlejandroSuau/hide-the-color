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

    private bool wasInDangerTime;
    private const int DANGER_TIME = 10;

    void Start()
    {
        wasInDangerTime = false;
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
                SwapFontColorIfNeeds(currentTime);
                UpdateTextTime();
                secondElapsing = 0;
            }
        }
    }

    void SwapFontColorIfNeeds(float secondElapsing)
    {        
        if (secondElapsing > DANGER_TIME && wasInDangerTime) {
            wasInDangerTime = false;
            SetNormalColor();
        } else if (secondElapsing <= DANGER_TIME && !wasInDangerTime) {
            wasInDangerTime = true;
            SetDangerColor();
        }
    }

    void SetNormalColor()
    {
        textTime.color = Color.white;
        textSeconds.color = Color.white;
    }

    void SetDangerColor()
    {
        textTime.color = Color.red;
        textSeconds.color = Color.red;
    }

    void UpdateTextTime()
    {
        textSeconds.text = (Mathf.Ceil(currentTime)).ToString();
    }

    public bool HasEnded()
    {
        return currentTime <= 0;
    }

    void Reset()
    {
        currentTime = startingTime;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool getIsPaused()
    {
        return this.isPaused;
    }
}
