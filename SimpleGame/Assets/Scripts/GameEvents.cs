using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static  GameEvents current;
    
    public event Action OnStartGame;
    public event Action OnStopGame;
    public event Action OnPauseGame;
    public event Action OnResumeGame;
    public event Action WinGame;

    void Awake()
    {
        current = this;
    }

    public void StartGameEvent()
    {
        OnStartGame?.Invoke();
    }

    public void StopGameEvent()
    {
        OnStopGame?.Invoke();
    }

    public void PauseGameEvent()
    {
        OnPauseGame?.Invoke();
    }

    public void ResumeGameEvent()
    {
        OnResumeGame?.Invoke();
    }

    public void WinGameEvent()
    {
        WinGame?.Invoke();
    }

}
