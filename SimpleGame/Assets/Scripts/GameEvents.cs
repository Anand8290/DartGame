using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static  GameEvents current;
    
    public event Action OnStartGame;
    public event Action OnStopGame;

    void Awake()
    {
        current = this;
    }

    public void StarGameEvent()
    {
        OnStartGame?.Invoke();
    }

    public void StopGameEvent()
    {
        OnStopGame?.Invoke();
    }

}
