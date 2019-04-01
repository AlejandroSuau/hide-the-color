using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float timeToChangeColor = 1.5f;
    private IEnumerator changingColorCoroutine;
    
    private int damage = 1;
    private GameColor color;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        changingColorCoroutine = ChangeColor(timeToChangeColor);
        StartCoroutine(changingColorCoroutine);
    }

    void Start()
    {
        SetColor(ColorsManager.instance.GetRandomColor());
    }

    public void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + GetSpriteName());
    }

    string GetSpriteName()
    {
        return "Player-" + color;
    }

    public int GetDamage()
    {
        return damage;
    }

    public GameColor GetColor()
    {
        return color;
    }

    private IEnumerator ChangeColor(float waitTime)
    {
        while(true) {
            yield return new WaitForSeconds(waitTime);
            SetColor(ColorsManager.instance.GetRandomColorDistinctTo(color));
        }
    }
}
