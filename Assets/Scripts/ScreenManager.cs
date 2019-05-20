using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    public GameObject playerGO;
    public Canvas gameScreenUI;
    
    private CountdownTimer countdownTimerScript;
    private Player playerScript;
    private EggsPanel eggsPanelScript;
    private ScreenButtons screenButtonsScript; // First is paused menu

    public EggsPanel EggsPanelScript { get { return eggsPanelScript; } }
    public Player PlayerScript { get { return playerScript; } }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one ScreenManager in scene!");
            return;
        }

        instance = this;
        
        GamePreservedStats.instance.ResetStats();
        
        countdownTimerScript = gameScreenUI.GetComponent<CountdownTimer>();
        eggsPanelScript = GetComponent<EggsPanel>();

        // Initialize the player's color always to a correct one.
        playerScript = playerGO.GetComponent<Player>();
        GameColor firstEggColor = eggsPanelScript.GetEggs()[0].Color;
        playerScript.ChangeToADesiredColor(firstEggColor);

        screenButtonsScript = GetComponentInChildren<ScreenButtons>();

        screenButtonsScript.Pause();
    }

    void Update()
    {
        // Do not allow to click if the game is paused.
        if (screenButtonsScript.getGameIsPaused() || playerScript.IsDead) return;

        if (Input.GetMouseButtonDown(0)) {
            BasicEgg egg = GetEggIfTouched();
            if(egg != null) {
                eggsPanelScript.TouchEgg(egg, playerScript);

                if (playerScript.IsDead) {
                    EndGame();
                }
            }
        }

        if (countdownTimerScript.HasEnded()) {
            EndGame();
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

    void EndGame()
    {
        GamePreservedStats.instance.gameSuccess = !playerScript.IsDead;
        GamePreservedStats.instance.eggs = eggsPanelScript.DestroyedEggs;
        
        if (playerScript.IsDead)
            GamePreservedStats.instance.diedTime = countdownTimerScript.GetCurrentTime();

        Invoke("LoadGameOverScene", 0.5f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
