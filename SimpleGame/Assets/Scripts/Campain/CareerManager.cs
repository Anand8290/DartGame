using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerManager : MonoBehaviour
{
   
    private bool gameOver = false;
    private bool startGame = false;
    [SerializeField] GameObject gameoverPanel, ThrowButton, PauseButton;
    [SerializeField] GameObject player, target;
    [SerializeField] GpgsAchievement gAchievement;

    void Start()
    {
        GameEvents.current.StarGameEvent();
    }


    public void GameOver(bool winGame, int star, int levelCoins)
    {
        gameOver = true;
        GameEvents.current.StopGameEvent();
        ThrowButton.SetActive(false);
        PauseButton.SetActive(false);
        gameoverPanel.GetComponent<GameOverPanel>().Animate(winGame, star, levelCoins);
    }


}
