using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColorBar : MonoBehaviour
{
    private Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;
    }

    public void DecrementBarWidth(float timeToChangeColor)
    {
        Vector3 temp = transform.localScale;
        temp.x -= Time.deltaTime/timeToChangeColor;
        transform.localScale = temp;
    }

    public void RestoreBarWidth()
    {
        transform.localScale = initialScale;
    }
}
