using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using System;

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
        config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        isInitialized = true;
        txtConsoleTxt.text = "GPS Initialized";
        SignInWithGoogle();
    }

    public void SignInWithGoogle()
    {
        
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

    

    #region SavedGames

    private bool isSaving;
    [SerializeField] Text txtTestData, txtTestDebug;
    
    public void OpenSave(bool saving)
    {
        txtTestDebug.text = "Open Save : " + saving;
        if(Social.localUser.authenticated)
        {
            txtTestDebug.text = "Local user authenticated";
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution("MyFileName", DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
        else
        {
            txtTestDebug.text = "Local user authentication failed!!!";
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            txtTestDebug.text = "Found Saved Game";
            if(isSaving)    
            {
                //we are saving through UI Button
                txtTestDebug.text = "Saving Game Data.....";
                byte[] myData = System.Text.ASCIIEncoding.ASCII.GetBytes(GetSaveString());
                SavedGameMetadataUpdate updatedMetadata = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved game at " + DateTime.Now.ToString()).Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, updatedMetadata, myData, OnSavedGameWritten);
                txtTestDebug.text = "my Data Commited to Cloud....";
            }
            else 
            {
                //we are loading through UI Button
                txtTestDebug.text = "Loading Game Data....";
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, OnSavedGameDataRead);
            }
        }
        
        else  
        {
            txtTestDebug.text = "Error Opening Save File!!!";
        }
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            Debug.Log("Successfuly saved to Cloud");
            txtTestDebug.text = "Successfuly Saved to Cloud";
        }
        else
        {
            // handle error
            Debug.Log("Failed to save in Cloud");
            txtTestDebug.text = "Error Saving to Cloud";
        }
    }

    private string GetSaveString()
    {
        string dataToSave = PlayerPrefs.GetFloat("HS_TA", 0).ToString("F1");
        //float testData2 = PlayerPrefs.GetFloat("HS_TA", 0);

        //dataToSave += testData1.ToString();
        //dataToSave += "|";
        //dataToSave += testData2.ToString("F1");
        //txtTestDebug.text = "Returning Data to Save : " + dataToSave;
        return dataToSave;
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            string loadedData = System.Text.ASCIIEncoding.ASCII.GetString(data);
            txtTestDebug.text = "Loaded raw data from Cloud : " + loadedData;
            LoadSaveString(loadedData);
        }
        else
        {
            // handle error
            txtTestDebug.text = "NO Save Data found!!!";
        }
    }

    private void LoadSaveString(string cloudData)
    {
        txtTestDebug.text = "try to load cloud data....";
        //string[] cloudStringArr = cloudData.Split('|');
        //Debug.Log("my Save and Loaded Data 1 : " + cloudStringArr[0]);
        //Debug.Log("my Save and Loaded Data 2 : " + cloudStringArr[1]);
        txtTestDebug.text = "Converted Cloud data loading....";
        //txtTestData.text = cloudStringArr[0] + " & " + cloudStringArr[1];
        txtTestData.text = cloudData;
        txtTestDebug.text = "Converted Cloud data loaded successfully";
    }
    #endregion

}
