using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTA : MonoBehaviour
{
    public bool canFire = true;
    public bool startGame = false;
    [SerializeField] TargetTA tarTA;
    
    [Header("Dart Setup")]
    [SerializeField] GameObject DartPrefab;
    [SerializeField] SpriteRenderer sR;
    [SerializeField] WindEffectTA windEffectTA;
    public int totalDarts = 100;
    public int thrownDarts = 0;
    public Text txtThrownDarts;

    [Header("Score")]
    public float totalScore = 0;
    private float[] score;
    public Text txtTotalScore;
    [SerializeField] Text[] txtDS;
    private int dsNumber = 0;

    [Header("Managers")]
    [SerializeField] GameManager gameMGR;
    
    void Awake()
    {
        SetupDartSprite();
        txtThrownDarts.text = thrownDarts.ToString();
        score = new float[totalDarts];

        // Subscribe to Game Evenets
        GameEvents.current.OnPauseGame += PauseGame;
        GameEvents.current.OnResumeGame += ResumeGame;
    }
    
    private void PauseGame()
    {
        canFire = false;
    }

    private void ResumeGame()
    {
        canFire = true;
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
        //Instantiate(DartPrefab, transform.position, Quaternion.identity);
        GameObject newDart =  Instantiate(DartPrefab, transform.position, Quaternion.identity);
        newDart.GetComponent<DartControlTA>().Fly(windEffectTA.windSpeed);
        sR.enabled = false;
        canFire = false;
        thrownDarts += 1;
        AudioManager.instance.PlaySound("Throw");
    }

    public void UpdateScore(float scoreAmt)
    {
        int i = thrownDarts;
        score[i] = scoreAmt;
        totalScore += score[i];
        txtTotalScore.text = totalScore.ToString("F0");
        txtThrownDarts.text = thrownDarts.ToString();
        
        // Score update on UI of last 3 throws
        txtDS[dsNumber].text = score[i].ToString("F0");
        txtDS[dsNumber].color = Color.yellow;
        if(dsNumber>0)
        {
           txtDS[dsNumber-1].color = Color.white;
        }
        else
        {
            txtDS[txtDS.Length-1].color = Color.white;
        }
        dsNumber += 1;
        if(dsNumber == txtDS.Length)
        {
            dsNumber = 0;
        }
        canFire = true;
        sR.enabled = true;
        windEffectTA.CreateRandomWind();
    }

    public void ThrowUIButton()
    {
        if(startGame & canFire)
        {
            Throw();
        }
    }

}
