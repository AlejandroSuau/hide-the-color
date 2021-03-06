﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFingersManager : MonoBehaviour
{
    public GameObject[] helperFingers;
    
    // Start is called before the first frame update
    void Start()
    {
        /* playerScript = player.GetComponent<Player>();
        eggsPanelsScript = eggsPanel.GetComponent<EggsPanel>();*/
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(BasicEgg egg in ScreenManager.instance.EggsPanelScript.GetEggs()) {
            bool active;
            if (egg != null && !egg.IsDead 
                && egg.IsTheSameColorAs(ScreenManager.instance.PlayerScript.Color)) {
                active= true;
            } else {
                active = false;
            }
            helperFingers[i].SetActive(active);
            i ++;
        }
    }
}
