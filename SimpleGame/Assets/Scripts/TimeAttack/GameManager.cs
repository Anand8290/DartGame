using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private bool gameOver = false;
    private bool startGame = false;
    
    [SerializeField] GameObject player, target;
    
    [Header("Score Setup")]
    [SerializeField] Text txtFinalScore;
    [SerializeField] Text txtHsTA;
    [SerializeField] Text txtAvgScore;
    private float totalScore, highScoreTA, dartsThrown, averageScore;
    
    [Header("Time Setup")]
    public float totalTime;
    public Text txtTimeRemaining;
    private float timeRemaining;
    [SerializeField] Image TimeupImg;

    [Header("Coins Setup")]
    public int totalCoins;
    [SerializeField] int levelCoins = 25;
    [SerializeField] Text txtEarnedCoins;
    
    [Header("Panels")]
    [SerializeField] GameObject GetReadyPanel;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject ThrowButton;
    [SerializeField] GameObject buttonHsLB;
    [SerializeField] GameObject WindTxt;
    [SerializeField] GameObject PauseButton;

    [Header("Managers")]
    [SerializeField] GpgsAchievement gAchievement;
    
    private int gamesPlayedTA;
    

    void Start()
    {
        timeRemaining = totalTime;
        txtTimeRemaining.text = timeRemaining.ToString("F0");
        WindTxt.SetActive(false);
        GetReadyPanel.SetActive(true);
        highScoreTA = PlayerPrefs.GetFloat("HS_TA", 0);
        gamesPlayedTA = PlayerPrefs.GetInt("GP_TA", 0);
        totalCoins = PlayerPrefs.GetInt("COINS", 0);
    }

    void Update()
    {
        if(!gameOver & startGame)
        {
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
    }

    public void StartGame()
    {
        startGame = true;
        WindTxt.SetActive(true);
        player.GetComponent<PlayerTA>().startGame = true;
        target.GetComponent<TargetTA>().stopMoving = false;
    }

    public void GameOver()
    {
        gameOver = true;
        target.GetComponent<TargetTA>().stopMoving = true;
        target.GetComponent<BoxCollider2D>().enabled = false;
        ThrowButton.SetActive(false);
        WindTxt.SetActive(false);
        player.SetActive(false);
        target.SetActive(false);
        PauseButton.SetActive(false);
        RewardCoins();
        StartCoroutine(TimeUp());
    }

    IEnumerator TimeUp()
    {
        TimeupImg.transform.gameObject.SetActive(false);
        TimeupImg.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        TimeupImg.transform.gameObject.SetActive(false);

        gameoverPanel.SetActive(true);

        totalScore = player.GetComponent<PlayerTA>().totalScore;
        dartsThrown = player.GetComponent<PlayerTA>().thrownDarts;
        averageScore = totalScore/dartsThrown;

        if(totalScore > highScoreTA)
        {
            highScoreTA = totalScore;
            PlayerPrefs.SetFloat("HS_TA", highScoreTA);
            buttonHsLB.SetActive(true);
        }
        
        txtFinalScore.text = totalScore.ToString("F0");
        txtHsTA.text = "High Score : " + highScoreTA.ToString("F0");
        txtAvgScore.text = averageScore.ToString("F1");
        txtEarnedCoins.text = "+ " + levelCoins.ToString();
        
        gamesPlayedTA += 1;
        PlayerPrefs.SetInt("GP_TA", gamesPlayedTA);
        
        if(gamesPlayedTA >= 1)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_newbie);
        }
        if(totalScore >= 50)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_rookie);
        }
        if(totalScore >= 150)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_the_arrow);
        }
        if(totalScore >= 250)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_the_artist);
        }  
    }

    public void ButtonSendHighScore()
    {
        gAchievement.SendScoreToLeaderboard(highScoreTA);
    }

    private float Divide(int u, int c)
    {
        int value = u / c;
        int remain = (u % c)* 100 / c;
        float result = value + (remain * 0.01f);
        return result;
    }

    public void RewardCoins()
    {
        totalCoins += levelCoins;
        PlayerPrefs.SetInt("COINS", totalCoins);
    }
 
}
