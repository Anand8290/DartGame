using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CareerManager : MonoBehaviour
{
   
    private bool gameOver = false;
    private bool startGame = false;
    [SerializeField] GameObject gameoverPanel, ThrowButton, PauseButton, WindDisplay;
    [SerializeField] GameObject player, target;
    [SerializeField] GpgsAchievement gAchievement;
    [SerializeField] LevelDBLoader levelDBLoader;

    private bool isLevelToUnlock = false;
    private int oldStars;

    void Start()
    {
        GameEvents.current.StartGameEvent();
        if(levelDBLoader.levelIndex > PlayerPrefs.GetInt("LEVELUNLOCKED", 0))
        {
            isLevelToUnlock = true;
        }

        oldStars = PlayerPrefs.GetInt("LEVELUNLOCKEDSTARS"+levelDBLoader.levelIndex,0);
    }


    public void GameOver(bool winGame, int star, int levelCoins)
    {
        StartCoroutine(GameOverNew(winGame, star, levelCoins));
        gameOver = true;
    }

    private IEnumerator GameOverNew(bool winGame, int star, int levelCoins)
    {
        if(winGame)
        {
            if(isLevelToUnlock)
            {
                PlayerPrefs.SetInt("LEVELUNLOCKED", levelDBLoader.levelIndex);
            }

            if(star > oldStars)
            {
                PlayerPrefs.SetInt("LEVELUNLOCKEDSTARS"+levelDBLoader.levelIndex, star);
            }
            GameEvents.current.WinGameEvent();
            yield return new WaitForSeconds(1.5f);
        }
        GameEvents.current.StopGameEvent();
        ThrowButton.SetActive(false);
        PauseButton.SetActive(false);
        WindDisplay.SetActive(false);
        gameoverPanel.GetComponent<GameOverPanel>().Animate(winGame, star, levelCoins);
    }


}
