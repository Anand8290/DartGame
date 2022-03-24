using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private bool gameOver = false;
    private bool startGame = false;
    [SerializeField] GameObject GetReadyPanel, gameoverPanel, ThrowButton;
    [SerializeField] GameObject buttonHsLB;
    [SerializeField] GameObject player, target;
    public float totalTime;
    public Text txtTimeRemaining;
    private float timeRemaining;
    private float totalScore, highScoreTA;

    [SerializeField] Text txtFinalScore, txtHsTA;
    [SerializeField] Image displayImg;
    [SerializeField] Sprite timeupSprite;

    [SerializeField] GpgsAchievement gAchievement;
    private int gamesPlayedTA;

    void Start()
    {
        timeRemaining = totalTime;
        txtTimeRemaining.text = timeRemaining.ToString("F0");
        GetReadyPanel.SetActive(true);
        highScoreTA = PlayerPrefs.GetFloat("HS_TA", 0);
        gamesPlayedTA = PlayerPrefs.GetInt("GP_TA", 0);
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
        player.GetComponent<PlayerTA>().startGame = true;
        target.GetComponent<TargetTA>().stopMoving = false;
    }

    public void GameOver()
    {
        gameOver = true;
        target.GetComponent<TargetTA>().stopMoving = true;
        target.GetComponent<BoxCollider2D>().enabled = false;
        ThrowButton.SetActive(false);
        player.SetActive(false);
        target.SetActive(false);
        StartCoroutine(TimeUp());
    }

    IEnumerator TimeUp()
    {
        displayImg.transform.gameObject.SetActive(false);
        displayImg.sprite = timeupSprite;
        displayImg.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        displayImg.transform.gameObject.SetActive(false);

        gameoverPanel.SetActive(true);

        totalScore = player.GetComponent<PlayerTA>().totalScore;

        if(totalScore > highScoreTA)
        {
            highScoreTA = totalScore;
            PlayerPrefs.SetFloat("HS_TA", highScoreTA);
            buttonHsLB.SetActive(true);
        }
        txtFinalScore.text = "Score : " + totalScore.ToString("F1");
        txtHsTA.text = "High Score : " + highScoreTA.ToString("F1");
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
    }

    public void ButtonSendHighScore()
    {
        gAchievement.SendScoreToLeaderboard(highScoreTA);
    }
 
}
