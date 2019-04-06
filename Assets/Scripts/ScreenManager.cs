using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject playerGO;
    
    private Player playerScript;
    private EggsPanel eggsPanel;

    void Start()
    {
        eggsPanel = GetComponent<EggsPanel>();
        playerScript = playerGO.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            BasicEgg egg = GetEggIfTouched();
            if(egg != null) {
                eggsPanel.TouchEgg(egg, playerScript);
            }
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
}
