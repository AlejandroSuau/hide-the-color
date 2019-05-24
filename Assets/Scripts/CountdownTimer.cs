using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text textTime;
    public Text textSeconds;
    
    public AudioClip audioWarningSecond;
    AudioSource audioSource;

    [SerializeField]
    private float startingTime;
    
    private float currentTime;
    private bool isPaused;
    private float secondElapsing;

    private bool wasInDangerTime;
    private const int DANGER_TIME = 5;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        wasInDangerTime = false;
        currentTime = startingTime;
        secondElapsing = 0;
        UpdateTextTime();
    }

    public void UpdateTimerBehaviour()
    {       
        if (!HasEnded() && !isPaused) {
            currentTime -= Time.deltaTime;
            
            secondElapsing += Time.deltaTime;
            if (secondElapsing >= 1 || currentTime <= 0) {
                SwapFontColorIfNeeds();
                UpdateTextTime();
                
                if (IsInWarningTime() && currentTime > 0)
                    PlayWarningSecondSound();

                secondElapsing = 0;
            }
        }
    }

    bool IsInWarningTime()
    {
        return currentTime <= DANGER_TIME;
    }

    void SwapFontColorIfNeeds()
    {        
        if (!IsInWarningTime() && wasInDangerTime) {
            wasInDangerTime = false;
            SetNormalColor();
        } else if (IsInWarningTime() && !wasInDangerTime) {
            wasInDangerTime = true;
            SetDangerColor();
        }
    }
    
    void UpdateTextTime()
    {
        textSeconds.text = (Mathf.Ceil(currentTime)).ToString();
    }

    void PlayWarningSecondSound()
    {
        audioSource.PlayOneShot(audioWarningSecond, 0.3f);
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
