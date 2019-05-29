using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerEgg : BasicEgg
{
    private string COLORCHANGER_EGG_TYPE_NAME = "Color-Changer";

    public override string SpriteName { get { return COLORCHANGER_EGG_TYPE_NAME + "-" + color; } }
    public override bool IsDead { get { return lifes <= 0; } }

    private EggsPanel eggsPanel;

    public override void SetColor(GameColor color)
    {
        this.color = color;
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/" + SpriteName);
    }

    public override void TakeDamage(int damage)
    {
        lifes -= damage;
        if (IsDead)
        {
            eggsPanel.SwitchAllAliveEggColors();
            Death();
        }
    }

    public override void SetEggsPanel(EggsPanel ep)
    {
        this.eggsPanel = ep;
    }
}
