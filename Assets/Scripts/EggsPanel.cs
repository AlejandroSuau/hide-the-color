using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsPanel : MonoBehaviour
{
    public GameObject[] platforms;
    
    private BasicEgg[] eggs;
    private int remainingEggs;

    const float extraEggPositionY =  0.70f;

    // Start is called before the first frame update
    void Start()
    {
        eggs = new BasicEgg[platforms.Length];
        remainingEggs = 0;
        SpawnEggs();
    }

    void SpawnEggs()
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
}
