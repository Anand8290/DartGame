using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool canFire = true;
    public GameObject DartPrefab;
    public int totalDarts = 3;
    int currentDarts;
    [SerializeField] SpriteRenderer sR;
    public Text txtCurrentDarts, txtTotalScore, txtWinScore;
    private float totalScore = 0;
    private float[] score;
    public UI_DartScore UIDartS;
    public float winScore;
    [SerializeField] GameObject gameoverPanel;
    private bool gameOver = false;
    private string result = "LOSE";
    [SerializeField] TargetScript tarScr;
    
    
    
    void Start()
    {
       SetupDartSprite();
       currentDarts = totalDarts;
       txtCurrentDarts.text = currentDarts.ToString();
       score = new float[totalDarts];
       txtWinScore.text = " / " + winScore;
    }

    void Update()
    {
        /*if(!gameOver)
        {
        if(Input.GetButtonDown("Fire1") & canFire & currentDarts > 0)
        {
            Throw();
        }
        }*/
    }

    private void Throw()
    {
        Instantiate(DartPrefab, transform.position, Quaternion.identity);
        sR.enabled = false;
        canFire = false;
        
    }


    private void SetupDartSprite()
    {
        SpriteRenderer srDart;
        srDart = DartPrefab.GetComponent<SpriteRenderer>();
        sR.sprite = srDart.sprite;
        sR.color = srDart.color;
        sR.gameObject.transform.localScale = srDart.transform.localScale;
    }

    public void UpdateScore(float scoreAmt)
    {
        int i = totalDarts-currentDarts;
        score[i] = scoreAmt;
        UIDartS.UpdateDartScore(i, score[i]);
        totalScore += score[i];
        txtTotalScore.text = totalScore.ToString("F1");
        currentDarts -= 1;
        txtCurrentDarts.text = currentDarts.ToString();
        canFire = true;

        if(totalScore >= winScore)
        {
            result = "WINNER";
            GameOver();
        }

        if(currentDarts>0)
        {
            sR.enabled = true;
        }
        else
        {
            GameOver();
        }

    }

    private void GameOver()
    {
        gameOver = true;
        tarScr.stopMoving = true;
        gameoverPanel.SetActive(true);
        gameoverPanel.transform.GetChild(0).GetComponent<Text>().text = result;
        if(result=="WINNER")
        gameoverPanel.transform.GetChild(0).GetComponent<Text>().color = Color.red;
    }

    public void ThrowUIButton()
    {
        if(canFire & currentDarts > 0 & !gameOver)
        {
            Throw();
        }
    }
}
