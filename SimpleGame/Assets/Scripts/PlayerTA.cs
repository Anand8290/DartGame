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
    public Text txtThrownDarts, txtTotalScore;
    public float totalScore = 0;
    private float[] score;
    public bool startGame = false;
    [SerializeField] TargetTA tarTA;
    [SerializeField] Text[] txtDS;
    private int dsNumber = 0;
    [SerializeField] GameManager gameMGR;
    
    
    void Awake()
    {
        SetupDartSprite();
        txtThrownDarts.text = thrownDarts.ToString();
        score = new float[totalDarts];
    }
    
    
    void Start()
    {
            
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
        thrownDarts += 1;
    }

    public void UpdateScore(float scoreAmt)
    {
        int i = thrownDarts;
        score[i] = scoreAmt;
        totalScore += score[i];
        txtTotalScore.text = totalScore.ToString("F1");
        txtThrownDarts.text = thrownDarts.ToString();
        
        // Score update on UI of last 3 throws
        txtDS[dsNumber].text = score[i].ToString("F1");
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
    }

    public void ThrowUIButton()
    {
        if(startGame & canFire)
        {
            Throw();
        }
    }

    

    
}
