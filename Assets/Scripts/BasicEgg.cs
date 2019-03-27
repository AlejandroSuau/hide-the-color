using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEgg : MonoBehaviour, IEgg
{
    public const string EGG_TYPE_NAME = "Basic";
    public EggColor eggColor;

    public void Touch()
    {
        Debug.Log("Touched {name: " + name + ", color: " + eggColor + "}");
    }
}
