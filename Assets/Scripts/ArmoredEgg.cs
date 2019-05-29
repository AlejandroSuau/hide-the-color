using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEgg : BasicEgg
{
    private string ARMORED_EGG_TYPE_NAME = "Armored";

    public override string SpriteName { get { return ARMORED_EGG_TYPE_NAME + "-" + color; } }
    public override bool IsDead { get { return lifes <= 0; } }

    public AudioClip audioDamaged;

    void Start()
    {
        this.lifes = 2;
    }

    public override void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead)
        {
            Death();
        } else
        {
            this.ARMORED_EGG_TYPE_NAME = "Armored-Damaged";
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
