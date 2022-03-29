using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour
{
    public bool canFire = true;
    public GameObject DartPrefab;
    public int totalDarts;
    private int thrownDarts = 0, dartsRemaining;
    [SerializeField] SpriteRenderer sR;
    public Text txtDartsRemaining, txtTotalScore, txtWinScore;
    public float totalScore = 0, winScore;
    private float[] score;
    private bool startGame = false, pauseGame = false;
    [SerializeField] Text[] txtDS;
    private int dsNumber = 0;
    [SerializeField] CareerManager careerMGR;
    [SerializeField] UI_DartScoreMgr UIDartS;
    private bool winGame = false;
    private int star1, star2, star3, star;
    
    
    void Awake()
    {
        
        SetupDartSprite();
        dartsRemaining = totalDarts;
        txtDartsRemaining.text = dartsRemaining.ToString();
        score = new float[totalDarts];
        txtWinScore.text = winScore.ToString();
        
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
        Instantiate(DartPrefab, transform.position, Quaternion.identity);
        sR.enabled = false;
        canFire = false;
        
    }

    public void UpdateScore(float scoreAmt)
    {
        /*int i = thrownDarts;
        score[i] = scoreAmt;
        totalScore += score[i];
        txtTotalScore.text = totalScore.ToString("F1");
        txtDartsRemaining.text = totalDarts.ToString();*/
        
        
        score[thrownDarts] = scoreAmt;
        UIDartS.UpdateDartScore(thrownDarts, score[thrownDarts]);
        totalScore += score[thrownDarts];
        txtTotalScore.text = totalScore.ToString("F1");
        thrownDarts += 1;
        dartsRemaining -= 1;
        txtDartsRemaining.text = dartsRemaining.ToString();
        
        if(totalScore >= winScore)
        {
            winGame = true;
            GameOver();
        }

        if(dartsRemaining>0)
        {
            sR.enabled = true;
            canFire = true;
        }
        else
        {
            GameOver();
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
        careerMGR.GameOver(winGame, star);
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
