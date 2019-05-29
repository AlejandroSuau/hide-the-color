using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float timeToChangeColor = 1.5f;
    private float currentTime;

    public AudioClip audioDeath;
    public AudioClip audioWin;
    AudioSource audioSource;

    private Animator lifeAnimator;

    private ChangingColorBar changingColorBar;

    private GameColor color;
    private int lifes = 1;
    private SpriteRenderer spriteRenderer;
    private EggsPanel eggsPanelScript;

    const int CONSECUTIVE_ERROR_COLORS = 1;
    int stackedIncorrectColors;

    public int Damage { get { return 1; } }
    public int Lifes { get { return lifes; } }
    public string SpriteName { get { return "Player-" + color; } }
    public GameColor Color { get { return color; } }
    public bool IsDead { get { return lifes <= 0; } }

    void Awake()
    {
        stackedIncorrectColors = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        changingColorBar = GetComponentInChildren<ChangingColorBar>();
        lifeAnimator = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdatePlayerBehaviour()
    {
        if (currentTime >= timeToChangeColor) {
            currentTime = 0;
            ChangeToRandomColor();
            //ScreenManager.instance.EggsPanelScript.AnimateEggsWithTheSameColorAs(color);
            changingColorBar.RestoreBarWidth();
        } else {
            currentTime += Time.deltaTime;
            changingColorBar.DecrementBarWidth(timeToChangeColor);
        }
    }

    public void ChangeToADesiredColor(GameColor newColor)
    {
        this.color = newColor;
        UpdateSprite();
    }

    public void ChangeToRandomColor()
    {
        GameColor newColor = ColorsManager.instance.GetRandomColorDistinctTo(color);
        if (!eggsPanelScript.DoesExistsInEggsThis(newColor)) 
        {
            if (stackedIncorrectColors == CONSECUTIVE_ERROR_COLORS)
            {
                stackedIncorrectColors = 0;
                newColor = eggsPanelScript.ObtainFirstCorrectColor();
            } else 
            {
                stackedIncorrectColors ++;
            }
        } else 
        {
            stackedIncorrectColors = 0;
        }

        ChangeToADesiredColor(newColor);
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead) {
            Death();
        }
    }

    public void Death()
    {
        audioSource.PlayOneShot(audioDeath, 0.5f);
        lifeAnimator.Play("LosingLife", 0, 0.0f);
    }

    public void Win()
    {
        audioSource.PlayOneShot(audioWin, 1f);
    }

    public void SetEggsPanelScript(EggsPanel epScript)
    {
        this.eggsPanelScript = epScript;
    }
}
