using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour, IEgg
{
    public const string EGG_TYPE_NAME = "Basic";
    
    private GameColor color;
    private SpriteRenderer spriteRenderer;
    private int lifes = 1; 

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Touch(int damage)
    {
        Debug.Log("Touched {name: " + name + ", color: " + color + "}");
        lifes -= damage;
        if (IsDeath())
            gameObject.SetActive(false);
    }

    public void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + GetSpriteName());
    }

    public bool IsDeath()
    {
        return lifes <= 0;
    }

    public string GetSpriteName()
    {
        return BasicEgg.EGG_TYPE_NAME + "-" + color;
    }
}
