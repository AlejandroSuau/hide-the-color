using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggsCounter : MonoBehaviour
{
    public Text eggsText;
    public Text eggsCounter;

    private int counter;

    void Start()
    {
        counter = 0;
        UpdateEggsCounterText();
    }

    public void IncrementCounter(int quantity)
    {
        counter += quantity;
        UpdateEggsCounterText();
    }

    void UpdateEggsCounterText()
    {
        eggsCounter.text = counter.ToString();
    }
}
