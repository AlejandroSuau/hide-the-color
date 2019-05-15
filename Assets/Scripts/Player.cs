using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float timeToChangeColor = 1.5f;
    private float currentTime;

    private ChangingColorBar changingColorBar;

    private GameColor color;
    private int lifes = 1;
    private SpriteRenderer spriteRenderer;

    public int Damage { get { return 1; } }
    public int Lifes { get { return lifes; } }
    public string SpriteName { get { return "Player-" + color; } }
    public GameColor Color { get { return color; } }
    public bool IsDead { get { return lifes <= 0; } }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        changingColorBar = GetComponentInChildren<ChangingColorBar>();
    }

    void Update()
    {
        if (currentTime >= timeToChangeColor) {
            currentTime = 0;
            ChangeToRandomColor();
            ScreenManager.instance.EggsPanelScript.AnimateEggsWithTheSameColorAs(color);
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
        ChangeToADesiredColor(newColor);
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
    }
}
