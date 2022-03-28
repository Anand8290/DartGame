using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerManager : MonoBehaviour
{
   
    private bool gameOver = false;
    private bool startGame = false;
    [SerializeField] GameObject gameoverPanel, ThrowButton;
    [SerializeField] GameObject player, target;
    private float totalScore;
    [SerializeField] GameObject[] StarsImage;
    [SerializeField] Text txtResult;
    [SerializeField] GpgsAchievement gAchievement;

    void Start()
    {
        GameEvents.current.StarGameEvent();
    }


    public void GameOver(bool winGame, int star)
    {
        gameOver = true;
        GameEvents.current.StopGameEvent();
        ThrowButton.SetActive(false);

        gameoverPanel.SetActive(true);
        if(winGame)
        {
            txtResult.text = "WINNER";
        }
        else
        {
            txtResult.text = "TRY AGAIN";
        }

        for(int i = 0; i < star; i++)
        {
            StarsImage[i].SetActive(true);
        }

    }


}
