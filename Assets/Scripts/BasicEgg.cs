using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour
{
    public const string EGG_TYPE_NAME = "Basic";
    
    private GameColor color;
    private int lifes = 1;
    private SpriteRenderer spriteRenderer;
    
    public int Damage { get { return 0; } }
    public int Lifes { get { return lifes; } }
    public string SpriteName { get { return BasicEgg.EGG_TYPE_NAME + "-" + color;; } }
    public GameColor Color { get { return color; } }
    public bool IsDead { get { return lifes <= 0; } }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsTheSameColorAs(GameColor color)
    {
        if (this.color == color) 
            return true;
        else 
            return false;
    }

    public void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead)
            Death();
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    void Death()
    {
        gameObject.SetActive(false);
    }

    public void AnimateCorrectColor()
    {
        GetComponent<Animator>().Play("CorrectColor", -1, 0.0f);
    }
}
