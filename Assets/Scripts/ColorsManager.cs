using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor
{
    Red,
    Yellow,
    Blue,
    Green
};

public class ColorsManager : MonoBehaviour
{
    public static ColorsManager instance = null;
    public GameColor[] usingColors;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public GameColor GetRandomColor() 
    {
        return usingColors[Random.Range(0, usingColors.Length)];
    }

    public GameColor GetRandomColorDistinctTo(GameColor color) 
    {
        GameColor newColor;
        do {
            newColor = GetRandomColor();
        } while (newColor == color);
        
        return newColor;
    }

    public GameColor[] GetUsingColors() 
    {
        return usingColors;
    }
}
