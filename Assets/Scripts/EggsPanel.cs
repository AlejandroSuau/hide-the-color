using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsPanel : MonoBehaviour
{
    public BasicEgg[] availableBasicEggs;

    public GameObject[] platforms;
    public BasicEgg[] eggs;
    int remainingEggs;

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

            eggs[i] = (BasicEgg) Instantiate(
                availableBasicEggs[Random.Range(0, availableBasicEggs.Length)],
                eggPosition, 
                Quaternion.identity);
            eggs[i].name = i + "-" + BasicEgg.EGG_TYPE_NAME;

            remainingEggs ++;
        }
    }
}
