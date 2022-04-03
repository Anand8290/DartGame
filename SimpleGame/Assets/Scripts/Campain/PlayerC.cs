using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour
{
    public bool canFire = true;
    public GameObject DartPrefab;
    private int totalDarts;
    private int thrownDarts = 0, dartsRemaining;
    [SerializeField] SpriteRenderer sR;
    public Text txtDartsRemaining, txtTotalScore, txtWinScore;
    private int winScore;
    private int liveScore;
    private int[] score;
    private bool startGame = false, pauseGame = false;
    [SerializeField] Text[] txtDS;
    [SerializeField] CareerManager careerMGR;
    [SerializeField] UI_DartScoreMgr UIDartS;
    private bool winGame = false;
    private int star1, star2, star3, star = 0;
    [SerializeField] LevelDBLoader levelDBLoader;
    CoinSystem coinSystem;
    [SerializeField] WindEffect windEffect;
    
    
    void Awake()
    {
        SetupDartSprite();
        totalDarts = levelDBLoader.darts;
        winScore = levelDBLoader.score;
        liveScore = winScore;
        dartsRemaining = totalDarts;
        txtDartsRemaining.text = dartsRemaining.ToString();
        score = new int[totalDarts];
        txtWinScore.text = liveScore.ToString();
        coinSystem = GetComponent<CoinSystem>();
        
        // Subscribe to Game Evenets
        GameEvents.current.OnStartGame += StartGame;
        GameEvents.current.OnStopGame += StopGame;
        GameEvents.current.OnPauseGame += PauseGame;
        GameEvents.current.OnResumeGame += ResumeGame;
    }

    private void StartGame()
    {
        startGame = true;
    }

    private void StopGame()
    {
        gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        pauseGame = true;
    }

    private void ResumeGame()
    {
        pauseGame = false;
    }

    private void SetupDartSprite()
    {
        SpriteRenderer srDart;
        srDart = DartPrefab.GetComponent<SpriteRenderer>();
        sR.sprite = srDart.sprite;
        sR.color = srDart.color;
        sR.gameObject.transform.localScale = srDart.transform.localScale;
    }

    private void Throw()
    {
        GameObject newDart =  Instantiate(DartPrefab, transform.position, Quaternion.identity);
        newDart.GetComponent<DartController>().Fly(windEffect.windSpeed);
        sR.enabled = false;
        canFire = false;
    }

    public void UpdateScore(int scoreAmt)
    {
        score[thrownDarts] = scoreAmt;
        UIDartS.UpdateDartScore(thrownDarts, score[thrownDarts]);
        liveScore -= score[thrownDarts];
        liveScore = Mathf.Clamp(liveScore, 0, winScore);
        txtWinScore.text = liveScore.ToString();
        thrownDarts += 1;
        dartsRemaining -= 1;
        txtDartsRemaining.text = dartsRemaining.ToString();
        
        if(liveScore <= 0)
        {
            winGame = true;
            GameOver();
        }
        else if(dartsRemaining <= 0)
        {
            GameOver();
        }
        else
        {
            sR.enabled = true;
            canFire = true;
            windEffect.CreateRandomWind();
        }
    }

    public void ThrowUIButton()
    {
        if(startGame & canFire & !pauseGame)
        {
            Throw();
        }
    }

    private void GameOver()
    {
        CalculateStar();
        coinSystem.CalculateCoins(star);
        careerMGR.GameOver(winGame, star, coinSystem.levelCoins);
    }

    private void CalculateStar()
    {
        
        float minDarts = winScore * 0.1f;
        float maxDarts = totalDarts;
        float diff = maxDarts - minDarts;
        
        star3 = Mathf.FloorToInt(minDarts + (diff * 0.33f));
        star2 = Mathf.CeilToInt(minDarts + (diff * 0.66f));
        star1 = Mathf.CeilToInt(minDarts + (diff * 1.0f));

        if(thrownDarts <= star3)
        {
            star = 3;
        }
        else if(thrownDarts <= star2)
        {
           star = 2; 
        }
        else if(thrownDarts <= star1)
        {
           star = 1;
        }

        if (!winGame)
        {
            star = 0;
        }
    }
    
}
