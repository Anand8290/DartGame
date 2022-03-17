using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class GooglePlayService : MonoBehaviour
{
    private PlayGamesClientConfiguration config;
    private bool signedIn = false, isInitialized = false;
    private float highScoreTA;
    [SerializeField] Text txtConsoleTxt;
    
    void Start()
    {
        Initialize();
    }


    void Initialize()
    {
        config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        isInitialized = true;
        txtConsoleTxt.text = "GPS Initialized";
        SignInWithGoogle();

    }

    public void SignInWithGoogle()
    {
        
        /*if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(signedIn)
        {
            return;
        }*/

        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
        // handle results
            switch(result)
            {
                case SignInStatus.Success:
                Debug.Log("Signed In");
                txtConsoleTxt.text = "Signed In";
                signedIn = true;
                PlayerPrefs.SetInt("GPGS", 1);
                break;

                default:
                Debug.Log("Sign In Failed");
                txtConsoleTxt.text = "Sign In Failed";
                PlayerPrefs.SetInt("GPGS", 0);
                break;
            }

        });
    }

    public void SignOutFromGoogle()
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        
        PlayGamesPlatform.Instance.SignOut();
        PlayerPrefs.SetInt("GPGS", 0);
        txtConsoleTxt.text = "Signed Out";
    }

    public void ShowLeaderboard()
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        
        // show leaderboard UI
        //Social.ShowLeaderboardUI();

        // show leaderboard UI
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_time_attack_leader);
    }

    

    public void SendScoreToLeaderboard()
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        long score = int.Parse(highScoreTA.ToString());
        Social.ReportScore(score, GPGSIds.leaderboard_time_attack_leader, (bool success) => {
        // handle success or failure
        });
    }

    public void ShowAcheivements()
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        // show achievements UI
        Social.ShowAchievementsUI();
    }


    public void UnlockAcheivement(string _achievement)
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        // unlock achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(_achievement, 100.0f, (bool success) => {
        // handle success or failure
        });
    }


    public void UnlockIncrementalAcheivement(string _achievement)
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        // increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
        PlayGamesPlatform.Instance.IncrementAchievement(_achievement, 1, (bool success) => {
            // handle success or failure
        });
    }

    public void RevealAcheivement(string _achievement)
    {
        if(!isInitialized)
        {
            Debug.Log("GPS is NOT Initialized");
            txtConsoleTxt.text = "GPS is NOT Initialized";
            return;
        }
        
        if(!signedIn)
        {
            return;
        }
        // Reveal achievement (achievement ID "Cfjewijawiu_QA")
        Social.ReportProgress(_achievement, 0.0f, (bool success) => {
        // handle success or failure
        });
    }
}
