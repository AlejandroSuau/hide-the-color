using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour, IEgg
{
    public const string EGG_TYPE_NAME = "Basic";
    
    private GameColor color;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Touch()
    {
        Debug.Log("Touched {name: " + name + ", color: " + color + "}");
    }

    public void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + GetSpriteName());
    }

    public string GetSpriteName()
    {
        return BasicEgg.EGG_TYPE_NAME + "-" + color;
    }
}
