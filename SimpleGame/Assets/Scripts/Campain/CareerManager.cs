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

    [Header ("Startup Panel")]
    [SerializeField] GameObject PanelStartup;
    [SerializeField] private Text txtToScore;
    [SerializeField] private Text txtInDarts;
    
    void Start()
    {
        //GameEvents.current.StartGameEvent();
        PauseButton.SetActive(false);
        StartCoroutine(PupupDisplay());
        if(levelDBLoader.levelIndex > PlayerPrefs.GetInt("LEVELUNLOCKED", -1))
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

    public IEnumerator PupupDisplay()
    {
        PanelStartup.SetActive(true);
        LeanTween.moveLocalX(PanelStartup, 0f, 1f).setDelay(0.5f).setEase(LeanTweenType.easeInCubic);
        txtToScore.text = levelDBLoader.score.ToString();
        txtInDarts.text = levelDBLoader.darts.ToString();
        LeanTween.moveLocalX(PanelStartup, 1400f, 1f).setDelay(3.5f).setEase(LeanTweenType.easeInOutExpo);
        yield return new WaitForSeconds(4.0f);
        PanelStartup.SetActive(false);
        GameEvents.current.StartGameEvent();
        PauseButton.SetActive(true);
    }
}
