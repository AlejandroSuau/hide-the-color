using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggsPanel : MonoBehaviour
{
    public Text eggsCounter;
    public GameObject[] platforms;
    public EggType[] eggTypes;
    
    AudioSource audioSource;
    public AudioClip audioPanelCleanned;

    private BasicEgg[] eggs;
    private int destroyedEggs;
    private int remainingEggs;

    public int DestroyedEggs { get { return destroyedEggs; } }
    public int RemainingEggs { get { return remainingEggs; } }
    public bool IsEmpty { get { return remainingEggs <= 0; } }

    const float WAITING_TIME_BEFORE_SPAWN_PANEL = 0.20f; 
    const float EXTRA_EGG_POSITION_Y =  0.70f;

    protected void Awake()
    {
        eggs = new BasicEgg[platforms.Length];
        destroyedEggs = 0;
        remainingEggs = 0;

        audioSource = GetComponent<AudioSource>();

        UpdateEggsCounterText();
        SpawnEggs();
    }

    public void SpawnEggs()
    {
        ShuffleEggTypes(ref eggTypes);
        for(int i = 0; i < eggs.Length; i++) {
            if (eggs[i] != null) eggs[i].DestroyGO();
            Vector3 eggPosition = platforms[i].GetComponent<Transform>().position;
            eggPosition.y += EXTRA_EGG_POSITION_Y;

            switch(eggTypes[i])
            {
                default:
                case EggType.BASIC:
                    BasicEgg basicEgg = (BasicEgg) Instantiate(
                        Resources.Load<BasicEgg>("Prefabs/BasicEgg"), eggPosition, Quaternion.identity);
                    basicEgg.SetColor(ColorsManager.instance.GetRandomColor());
                    basicEgg.name =  i + "-" + basicEgg.SpriteName;
                    eggs[i] = basicEgg;
                    break;
                case EggType.ARMORED:
                    ArmoredEgg armoredEgg = (ArmoredEgg) Instantiate(
                        Resources.Load<ArmoredEgg>("Prefabs/ArmoredEgg"), eggPosition, Quaternion.identity);
                    armoredEgg.SetColor(ColorsManager.instance.GetRandomColor());
                    armoredEgg.name =  i + "-" + armoredEgg.SpriteName;
                    eggs[i] = armoredEgg;
                    break;
                case EggType.COLORCHANGER:
                    ColorChangerEgg colorChangerEgg = (ColorChangerEgg) Instantiate(
                        Resources.Load<ColorChangerEgg>("Prefabs/ColorChangerEgg"), eggPosition, Quaternion.identity);
                    colorChangerEgg.SetEggsPanel(this);
                    colorChangerEgg.SetColor(ColorsManager.instance.GetRandomColor());
                    colorChangerEgg.name =  i + "-" + colorChangerEgg.SpriteName;
                    eggs[i] = colorChangerEgg;
                    break;
            }

            remainingEggs ++;
        }
    }

    public void TouchEgg(BasicEgg egg, Player player)
    {
        if (egg.IsDead) return;

        if (egg.IsTheSameColorAs(player.Color)) {
            egg.TakeDamage(player.Damage);
            
            if (egg.IsDead) {
                destroyedEggs ++;
                UpdateEggsCounterText();
                
                remainingEggs --;
                if(IsEmpty) {
                    audioSource.PlayOneShot(audioPanelCleanned, 0.5f);
                    Invoke("SpawnEggs", WAITING_TIME_BEFORE_SPAWN_PANEL);
                    //AnimateEggsWithTheSameColorAs( ScreenManager.instance.PlayerScript.Color);
                }
            }
        } else {
            player.TakeDamage(1);
        }
    }

    public GameColor ObtainFirstCorrectColor()
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead)
                return egg.Color;
        }
        
        return GameColor.Green;
    }

    public bool DoesExistsInEggsThis(GameColor color)
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead && egg.IsTheSameColorAs(color))
                return true;
        }

        return false;
    }

    protected void UpdateEggsCounterText()
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

    public BasicEgg[] GetEggs()
    {
        return this.eggs;
    }

    public void SwitchAllAliveEggColors()
    {
        foreach(BasicEgg egg in eggs) {
            if (!egg.IsDead) {
                egg.SetColor(ColorsManager.instance.GetRandomColorDistinctTo(egg.Color));
            }
        }
    }

    void ShuffleEggTypes(ref EggType[] eggTypes)
    {
        for(int i = eggTypes.Length - 1; i > 0; i--) {
            int r = Random.Range(0, i);
            EggType tmp = eggTypes[i];
            eggTypes[i] = eggTypes[r];
            eggTypes[r] = tmp;
        }
    }
}
