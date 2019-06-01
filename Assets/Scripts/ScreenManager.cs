using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private bool didGameFinished;

    public static ScreenManager instance;

    AudioSource audioSource;
    public AudioClip backgroundMusic;

    public GameObject playerGO;
    public GameObject medalsGO;
    public Canvas gameScreenUI;
    
    private float initialThreeCountdownSeconds = 3f;

    private CountdownTimer countdownTimerScript;
    private Player playerScript;
    private MedalsBehaviour medalsScript;
    private EggsPanel eggsPanelScript;
    private ScreenButtons screenButtonsScript; // First is paused menu

    public EggsPanel EggsPanelScript { get { return eggsPanelScript; } }
    public Player PlayerScript { get { return playerScript; } }

    void Awake()
    {
        didGameFinished = false;

        if (instance != null)
        {
            Debug.LogError("More than one ScreenManager in scene!");
            return;
        }

        instance = this;
        
        if (MenuGameMusicManager.instance != null && MenuGameMusicManager.instance.audioSource.isPlaying) { 
            StartCoroutine(AudioController.FadeOut(MenuGameMusicManager.instance.audioSource, 0.2f));
        }

        if (GameMusicManager.instance != null && !GameMusicManager.instance.audioSource.isPlaying) {
            StartCoroutine(AudioController.FadeIn(GameMusicManager.instance.audioSource, 1f));
        }

        GamePreservedStats.instance.ResetStats();
        
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(backgroundMusic, 0.1f);

        countdownTimerScript = gameScreenUI.GetComponent<CountdownTimer>();
        eggsPanelScript = GetComponent<EggsPanel>();

        // Initialize the player's color always to a correct one.
        playerScript = playerGO.GetComponent<Player>();
        GameColor firstEggColor = eggsPanelScript.GetEggs()[0].Color;
        playerScript.ChangeToADesiredColor(firstEggColor);
        playerScript.SetEggsPanelScript(eggsPanelScript);

        medalsScript = medalsGO.GetComponent<MedalsBehaviour>();

        screenButtonsScript = GetComponentInChildren<ScreenButtons>();
        screenButtonsScript.enabled = false;
    }

    void Update()
    {
        // 3 seconds initial countdown 
        if (initialThreeCountdownSeconds > 0) {
            initialThreeCountdownSeconds -= Time.deltaTime;
            return;
        } else {
            screenButtonsScript.enabled = true;
        }

        // Do not allow to click if the game is paused.
        if (didGameFinished || screenButtonsScript.getGameIsPaused() || playerScript.IsDead) return;

        // Control Time updates
        countdownTimerScript.UpdateTimerBehaviour();

        // Controls player updates
        playerScript.UpdatePlayerBehaviour();

        if (Input.GetMouseButtonDown(0)) {
            BasicEgg egg = GetEggIfTouched();
            if(egg != null) {
                eggsPanelScript.TouchEgg(egg, playerScript);

                // Evaluate if those number of eggs gives a new medal
                medalsScript.DoesItObtainsANewMedalWithThis(eggsPanelScript.DestroyedEggs);

                if (playerScript.IsDead) {
                    EndGame();
                }
            }
        }

        if (countdownTimerScript.HasEnded() && !didGameFinished) {
            didGameFinished = true;

            if (medalsScript.ObtainedMedals == 0)
                playerScript.Death();
            else
                playerScript.Win();

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
        // Stops background music.
        if (GameMusicManager.instance != null) {
            StartCoroutine(AudioController.FadeOut(GameMusicManager.instance.audioSource, 1f));
        }

        GamePreservedStats.instance.gameSuccess = (!playerScript.IsDead && medalsScript.ObtainedMedals > 0);
        GamePreservedStats.instance.eggs = eggsPanelScript.DestroyedEggs;
        GamePreservedStats.instance.medals = medalsScript.ObtainedMedals;
        
        int sceneId = int.Parse((SceneManager.GetActiveScene().name.Split('-')[1]));
        GamePreservedStats.instance.idLevel = sceneId;
        
        if (playerScript.IsDead)
            GamePreservedStats.instance.diedTime = countdownTimerScript.GetCurrentTime();

        Invoke("LoadGameOverScene", 2f);
    }

    void LoadGameOverScene()
    {
        if (GamePreservedStats.instance.gameSuccess)
        {
            SceneManager.LoadScene("GameCompleted");
        } else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
