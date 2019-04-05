using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject changingBar;
    private Transform changingBarTransform;
    private Vector3 changingBarInitScale;

    public float timeToChangeColor = 1.5f;
    private float currentTime;
    private int damage = 1;
    private GameColor color;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        changingBar.SetActive(true);
        changingBarTransform = changingBar.GetComponent<Transform>();
        changingBarInitScale = changingBarTransform.localScale;
    }

    void Start()
    {
        SetColor(ColorsManager.instance.GetRandomColor());
    }

    void Update()
    {
        if (currentTime >= timeToChangeColor) {
            currentTime = 0;
            SetColor(ColorsManager.instance.GetRandomColorDistinctTo(color));
            RestoreBarWidth();
        } else {
            currentTime += Time.deltaTime;
            DecrementBarWidth();
        }
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

    void RestoreBarWidth()
    {
        changingBarTransform.localScale = changingBarInitScale;
    }

    void DecrementBarWidth()
    {
        Vector3 temp = changingBarTransform.localScale;
        temp.x -= Time.deltaTime/timeToChangeColor;
        changingBarTransform.localScale = temp;
    }
}
