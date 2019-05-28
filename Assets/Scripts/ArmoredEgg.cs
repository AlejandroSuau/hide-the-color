using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEgg : BasicEgg
{
    private string EGG_TYPE_NAME = "Armored";
    protected int lifes = 2;

    public virtual string SpriteName { get { return EGG_TYPE_NAME + "-" + color; } }
    public override bool IsDead { get { return lifes <= 0; } }

    public AudioClip audioDamaged;

    public override void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead)
        {
            Death();
        } else
        {
            EGG_TYPE_NAME += "-Damaged";
            audioSource.PlayOneShot(audioDamaged, 0.5f);
            SetColor(this.color);   
        }
    }

    public override void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }
}
