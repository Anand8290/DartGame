using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTA : MonoBehaviour
{
    public bool canFire = true;
    public GameObject DartPrefab;
    public int totalDarts = 100;
    int thrownDarts = 0;
    [SerializeField] SpriteRenderer sR;
    public Text txtThrownDarts, txtTotalScore, txtTimeRemaining;
    private float totalScore = 0;
    private float[] score;
    public float totalTime;
    private float timeRemaining;
    [SerializeField] GameObject gameoverPanel;
    private bool gameOver = false;
    private string result = "GAME OVER";
    [SerializeField] TargetTA tarTA;
    [SerializeField] Text[] txtDS;
    private int dsNumber = 0;
    
    
    
    void Start()
    {
       SetupDartSprite();
       timeRemaining = totalTime;
       txtThrownDarts.text = thrownDarts.ToString();
       score = new float[totalDarts];
       txtTimeRemaining.text = timeRemaining.ToString("F0");
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

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            txtTimeRemaining.text = timeRemaining.ToString("F0");
        }
        else
        {
            GameOver();
        }

    }

    private void Throw()
    {
        Instantiate(DartPrefab, transform.position, Quaternion.identity);
        sR.enabled = false;
        canFire = false;
        thrownDarts += 1;
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
        int i = thrownDarts;
        score[i] = scoreAmt;
        totalScore += score[i];
        txtTotalScore.text = totalScore.ToString("F1");
        txtThrownDarts.text = thrownDarts.ToString();
        txtDS[dsNumber].text = score[i].ToString("F1");
        txtDS[dsNumber].color = Color.blue;
        txtDS[dsNumber].GetComponentInParent<Image>().color = Color.yellow;
        if(dsNumber>0)
        {
           txtDS[dsNumber-1].color = Color.black;
           txtDS[dsNumber-1].GetComponentInParent<Image>().color = Color.white;
        }
        else
        {
            txtDS[txtDS.Length-1].color = Color.black;
            txtDS[txtDS.Length-1].GetComponentInParent<Image>().color = Color.white;
        }
        dsNumber += 1;
        if(dsNumber == txtDS.Length)
        {
            dsNumber = 0;
        }
        canFire = true;
        sR.enabled = true;
    }

    private void GameOver()
    {
        gameOver = true;
        tarTA.stopMoving = true;
        tarTA.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameoverPanel.SetActive(true);
        gameoverPanel.transform.GetChild(0).GetComponent<Text>().text = result;
    }

    public void ThrowUIButton()
    {
        if(canFire & !gameOver)
        {
            Throw();
        }
}
}
