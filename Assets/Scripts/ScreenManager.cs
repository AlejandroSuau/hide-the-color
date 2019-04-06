using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject playerGO;
    public Canvas gameScreenUI;
    
    private CountdownTimer countdownTimerScript;
    private Player playerScript;
    private EggsPanel eggsPanelScript;

    void Start()
    {
        GamePreservedStats.instance.ResetStats();
        
        countdownTimerScript = gameScreenUI.GetComponent<CountdownTimer>();
        eggsPanelScript = GetComponent<EggsPanel>();
        playerScript = playerGO.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            BasicEgg egg = GetEggIfTouched();
            if(egg != null) {
                eggsPanelScript.TouchEgg(egg, playerScript);

                if (playerScript.IsDead())
                    EndGame(false);
            }
        }

        if (countdownTimerScript.HasEnded()) {
            EndGame(true);
        }
    }

    BasicEgg GetEggIfTouched()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider == null) {
            return null;
        } else {
            BasicEgg touchedEgg = (BasicEgg) hit.collider.gameObject.GetComponent(typeof(BasicEgg));
            return touchedEgg;
        }
    }

    void EndGame(bool success)
    {
        GamePreservedStats.instance.gameSuccess = success;
        GamePreservedStats.instance.eggs = eggsPanelScript.GetDestroyedEggs();
        
        if (!success)
            GamePreservedStats.instance.diedTime = countdownTimerScript.GetCurrentTime();
    }
}
