using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggsPanel : MonoBehaviour
{
    public Text eggsCounter;
    public GameObject[] platforms;
    
    AudioSource audioSource;
    public AudioClip audioPanelCleanned;

    private BasicEgg[] eggs;
    private int destroyedEggs;
    private int remainingEggs;

    public int DestroyedEggs { get { return destroyedEggs; } }
    public int RemainingEggs { get { return remainingEggs; } }
    public bool IsEmpty { get { return remainingEggs <= 0; } }

    const float WAITING_TIME_BEFORE_SPAWN_PANEL = 0.20f; 
    const float EXTRA_EGG_POSITION_Y =  0.70f;

    protected void Awake()
    {
        eggs = new BasicEgg[platforms.Length];
        destroyedEggs = 0;
        remainingEggs = 0;

        audioSource = GetComponent<AudioSource>();

        UpdateEggsCounterText();
        SpawnEggs();
    }

    public void SpawnEggs()
    {
        for(int i = 0; i < eggs.Length; i++) {
            if (eggs[i] != null) eggs[i].DestroyGO();
            Vector3 eggPosition = platforms[i].GetComponent<Transform>().position;
            eggPosition.y += EXTRA_EGG_POSITION_Y;

            /* BasicEgg newEgg = (BasicEgg) Instantiate(
                Resources.Load<BasicEgg>("Prefabs/BasicEgg"), eggPosition, Quaternion.identity);*/

            ArmoredEgg newEgg = (ArmoredEgg) Instantiate(
                Resources.Load<ArmoredEgg>("Prefabs/ArmoredEgg"), eggPosition, Quaternion.identity);

            newEgg.SetColor(ColorsManager.instance.GetRandomColor());
            newEgg.name =  i + "-" + newEgg.SpriteName;

            eggs[i] = newEgg;
            remainingEggs ++;
        }
    }

    public void TouchEgg(BasicEgg egg, Player player)
    {
        if (egg.IsDead) return;

        if (egg.IsTheSameColorAs(player.Color)) {
            egg.TakeDamage(player.Damage);
            
            if (egg.IsDead) {
                destroyedEggs ++;
                UpdateEggsCounterText();
                
                remainingEggs --;
                if(IsEmpty) {
                    audioSource.PlayOneShot(audioPanelCleanned, 0.5f);
                    Invoke("SpawnEggs", WAITING_TIME_BEFORE_SPAWN_PANEL);
                    //AnimateEggsWithTheSameColorAs( ScreenManager.instance.PlayerScript.Color);
                }
            }
        } else {
            player.TakeDamage(1);
        }
    }

    public GameColor ObtainFirstCorrectColor()
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead)
                return egg.Color;
        }
        
        return GameColor.Green;
    }

    public bool DoesExistsInEggsThis(GameColor color)
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead && egg.IsTheSameColorAs(color))
                return true;
        }

        return false;
    }

    protected void UpdateEggsCounterText()
    {
        eggsCounter.text = destroyedEggs.ToString();
    }

    public void AnimateEggsWithTheSameColorAs(GameColor color)
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead)
                egg.AnimateIfIsCorrectColor(color);
        }
    }

    public BasicEgg[] GetEggs()
    {
        return this.eggs;
    }
}
