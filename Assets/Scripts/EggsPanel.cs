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

    public int DestroyedEggs { get { return destroyedEggs; } }
    public int RemainingEggs { get { return remainingEggs; } }
    public bool IsEmpty { get { return remainingEggs <= 0; } }

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

            newEgg.AnimateIfIsCorrectColor(ScreenManager.instance.PlayerScript.Color);

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

    public void TouchEgg(BasicEgg egg, Player player)
    {
        if (egg.IsTheSameColorAs(player.Color)) {
            egg.TakeDamage(player.Damage);
            
            if (egg.IsDead) {
                destroyedEggs ++;
                UpdateEggsCounterText();
                
                remainingEggs --;
                if(IsEmpty) {
                    CleanPanel();
                    SpawnEggs();
                }
            }
        } else {
            player.TakeDamage(1);
        }
    }

    void UpdateEggsCounterText()
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
}
