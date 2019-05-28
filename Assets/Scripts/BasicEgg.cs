using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour
{
    protected const string EGG_TYPE_NAME = "Basic";
    
    public AudioClip audioDeath;

    protected AudioSource audioSource;

    protected GameColor color;
    protected int lifes = 1;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    
    public int Damage { get { return 0; } }
    public int Lifes { get { return lifes; } }
    public string SpriteName { get { return EGG_TYPE_NAME + "-" + color; } }
    public GameColor Color { get { return color; } }
    public bool IsDead { get { return lifes <= 0; } }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsTheSameColorAs(GameColor color)
    {
        return (this.color == color);
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
        audioSource.PlayOneShot(audioDeath, 0.5f);
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Destroying-Egg-Animation-3");
        animator.Play("Dead", 0, 0.0f);
    }

    public void AnimateIfIsCorrectColor(GameColor color)
    {
        if (IsTheSameColorAs(color))
            AnimateCorrectColor();
        else
            AnimateStatic();
    }

    public void AnimateCorrectColor()
    {
        animator.Play("CorrectColor", -1, 0.0f);
    }

    public void AnimateStatic()
    {
        animator.Play("Static", -1, 0.0f);
    }
}
