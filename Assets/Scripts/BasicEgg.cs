using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour
{
    private string BASIC_EGG_TYPE = "Basic";
    
    public AudioClip audioDeath;

    protected AudioSource audioSource;

    protected GameColor color;
    protected int lifes = 1;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    
    public int Damage { get { return 0; } }
    public int Lifes { get { return lifes; } }
    public virtual string SpriteName { get { return BASIC_EGG_TYPE + "-" + color; } }
    public virtual GameColor Color { get { return color; } }
    public virtual bool IsDead { get { return lifes <= 0; } }

    public virtual void SetEggsPanel(EggsPanel ep) {}

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsTheSameColorAs(GameColor color)
    {
        return (this.color == color);
    }

    public virtual void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }

    public virtual void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead)
            Death();
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    protected void Death()
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
