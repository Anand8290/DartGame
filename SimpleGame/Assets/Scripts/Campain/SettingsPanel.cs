using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    private bool mute = false;

    [SerializeField] GameObject MuteButton, UnmuteButton;
    [SerializeField] GameObject RemoveAdsButton;

    void Awake()
    {
       UpdateMuteButton();
       UpdateRemoveAdsButton();
    }

    private void UpdateMuteButton()
    {
        if(PlayerPrefs.GetInt("MUTE", 0) == 1)
        {
            mute = true;
        }
        else
        {
            mute = false;
        }
        MuteButton.SetActive(!mute);
        UnmuteButton.SetActive(mute);
    }

    public void Mute()
	{
		AudioManager.instance.Mute();
        UpdateMuteButton();
	}

    public void UnMute()
	{
		AudioManager.instance.UnMute();
        UpdateMuteButton();
	}

    public void UpdateRemoveAdsButton()
    {
        if(PlayerPrefs.GetInt("REMOVEADS", 0) == 1)
        {
            RemoveAdsButton.SetActive(false);
        }
        else
        {
            RemoveAdsButton.SetActive(true);
        }
    }
}
