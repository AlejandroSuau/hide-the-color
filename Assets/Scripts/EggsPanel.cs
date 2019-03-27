using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsPanel : MonoBehaviour
{
    public GameObject[] availableBasicEggs;

    public GameObject[] floors;
    public GameObject[] eggs;
    int remainingEggs;

    const float extraEggPositionY =  0.70f;
    const string basicEggName = "BasicEgg";

    // Start is called before the first frame update
    void Start()
    {
        eggs = new GameObject[floors.Length];
        remainingEggs = 0;
        SpawnEggs();
    }

    void SpawnEggs()
    {
        for(int i = 0; i < eggs.Length; i++) {
            Vector3 eggPosition = floors[i].GetComponent<Transform>().position;
            eggPosition.y += extraEggPositionY;

            eggs[i] = Instantiate(availableBasicEggs[Random.Range(0, availableBasicEggs.Length)],
                                    eggPosition, 
                                    Quaternion.identity);
            eggs[i].name = i + "-" + basicEggName;

            remainingEggs ++;
        }
    }
}
