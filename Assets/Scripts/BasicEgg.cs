using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour
{
    public const string EGG_TYPE_NAME = "Basic";
    
    private GameColor color;
    private SpriteRenderer spriteRenderer;
    private int lifes = 1; 
    private int damage = 1;

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
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + GetSpriteName());
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead())
            Death();
    }

    public bool IsDead()
    {
        return lifes <= 0;
    }

    public string GetSpriteName()
    {
        return BasicEgg.EGG_TYPE_NAME + "-" + color;
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
