using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggsPanel : MonoBehaviour
{
    public Text eggsCounter;
    public GameObject[] platforms;
    
    private BasicEgg[] eggs;
    private int destroyedEggs;
    private int remainingEggs;

    const float extraEggPositionY =  0.70f;

    void Start()
    {
        eggs = new BasicEgg[platforms.Length];
        destroyedEggs = 0;
        remainingEggs = 0;
        UpdateEggsCounterText();
        SpawnEggs();
    }

    public void SpawnEggs()
    {
        for(int i = 0; i < eggs.Length; i++) {
            Vector3 eggPosition = platforms[i].GetComponent<Transform>().position;
            eggPosition.y += extraEggPositionY;

            BasicEgg newEgg = (BasicEgg) Instantiate(
                Resources.Load<BasicEgg>("Prefabs/BasicEgg"), eggPosition, Quaternion.identity);
            newEgg.name =  i + "-" + BasicEgg.EGG_TYPE_NAME;
            newEgg.SetColor(ColorsManager.instance.GetRandomColor());
            
            eggs[i] = newEgg;
            remainingEggs ++;
        }
    }

    public void CleanPanel()
    {
        for(int i = 0; i < eggs.Length; i++) {
            eggs[i].DestroyGO();
            eggs[i] = null;
        }
    }

    public bool IsEmpty()
    {
        return remainingEggs <= 0;
    }

    public void TouchEgg(BasicEgg egg, Player player)
    {
        if (egg.IsTheSameColorAs(player.GetColor())) {
            egg.TakeDamage(player.GetDamage());
            
            if (egg.IsDead()) {
                destroyedEggs ++;
                UpdateEggsCounterText();
                
                remainingEggs --;
                if(IsEmpty()) {
                    CleanPanel();
                    SpawnEggs();
                }
            }
        } else {
            player.TakeDamage(egg.GetDamage());
        }
    }

    void UpdateEggsCounterText()
    {
        eggsCounter.text = destroyedEggs.ToString();
    }

    public int GetDestroyedEggs()
    {
        return destroyedEggs;
    }
}
