using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCountdownBehaviour : MonoBehaviour
{
    public Canvas backgroundCanvas;
    public GameObject[] numbers;

    public void Start()
    {       
        Invoke("PlayNumberThreeAnimation", 0.0f);
        Invoke("PlayNumberTwoAnimation", 1.0f);
        Invoke("PlayNumberOneAnimation", 2.0f);
        Invoke("DisableStartingCountdown", 3.0f);
    }

    void PlayNumberThreeAnimation()
    {
        numbers[2].SetActive(true);
        numbers[2].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void PlayNumberTwoAnimation()
    {
        numbers[1].SetActive(true);
        numbers[1].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void PlayNumberOneAnimation()
    {
        numbers[0].SetActive(true);
        numbers[0].GetComponent<Animator>().Play("InitialCountdownReduction", 0, 0.0f);
    }

    void DisableStartingCountdown()
    {
        Destroy(gameObject);
    }
}
