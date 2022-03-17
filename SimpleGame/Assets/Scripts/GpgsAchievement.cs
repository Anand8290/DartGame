using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;


public class GpgsAchievement : MonoBehaviour
{
    private int isSignedIn;

    void Start()
    {
        isSignedIn = PlayerPrefs.GetInt("GPGS", 0);
    }
    
    public void ShowAcheivements()
    {
        if(isSignedIn !=1)
        return;
        Social.ShowAchievementsUI();
    }


    public void UnlockAcheivement(string _achievement)
    {
        if(isSignedIn !=1)
        return;
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(_achievement, 100.0f, (bool success) => {
        // handle success or failure
        });
    }


    public void UnlockIncrementalAcheivement(string _achievement)
    {
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(_achievement, 1, (bool success) => {
            // handle success or failure
        });
    }

    public void RevealAcheivement(string _achievement)
    {
        // Reveal achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(_achievement, 0.0f, (bool success) => {
        // handle success or failure
        });
    }

    public void ShowLeaderboard()
    {
        if(isSignedIn !=1)
        return;

        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_time_attack_leader);
    }

    public void SendScoreToLeaderboard(float highScore)
    {
        string[] shsarray = highScore.ToString("F1").Split('.');
        int hs = int.Parse(shsarray[0] + shsarray[1]);
        Debug.Log("new highscore : " + hs);

        
        if(isSignedIn !=1)
        return;
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        
        Social.ReportScore(hs, GPGSIds.leaderboard_time_attack_leader, (bool success) => {
        // handle success or failure
        if(success)
        {
            Debug.Log("post success");
            ShowLeaderboard();
        }
        else
        {
            Debug.Log("post failed");
        }
        });
    }
}
