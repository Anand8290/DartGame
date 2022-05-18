using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel, Header, Coins, Button1, Button2, Button3_RewardAd;
    [SerializeField] Text txtResult, txtLevelCoins;
    [SerializeField] GameObject[] Stars, StarsFill;
    [SerializeField] InterstitialAdManager interstitialAdManager;
    [SerializeField] GameObject Cup;

    private int SHOWAD;
    
    void Awake()
    {
        Cup.GetComponent<Image>().enabled = false;
        SHOWAD = PlayerPrefs.GetInt("SHOWAD", 0);
        SHOWAD +=1;
        PlayerPrefs.SetInt("SHOWAD", SHOWAD);
    }

    public void Animate(bool winGame, int star, int levelCoins)
    {
        Panel.SetActive(true);

        if(winGame)
        {
            txtResult.text = "WINNER";
            Cup.GetComponent<Image>().enabled = true;
        }
        else
        {
            txtResult.text = "TRY AGAIN";
            Cup.GetComponent<Image>().enabled = false;
        }
        
        Button3_RewardAd.SetActive(winGame);

        for(int i = 0; i < star; i++)
        {
            StarsFill[i].SetActive(true);
        }

        txtLevelCoins.text = "+" + levelCoins;

        Header.transform.localScale = new Vector3(0f, 0f, 0f);
        for(int i = 0; i < Stars.Length; i++)
        {
            Stars[i].transform.localScale = new Vector3(0f, 0f, 0f);
        }
        Button1.transform.localScale = new Vector3(0f, 0f, 0f);
        Button2.transform.localScale = new Vector3(0f, 0f, 0f);
        Button3_RewardAd.transform.localScale = new Vector3(0f, 0f, 0f);
        Cup.transform.localScale = new Vector3(0f, 0f, 0f);

        LeanTween.alpha(Coins.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.moveLocal(Panel, new Vector3(0f, 0f, 0f), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(Header, new Vector3(1f, 1f, 1f), 1.0f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Cup, new Vector3(1.25f, 1.25f, 1f), 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.alpha(Coins.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(2.0f);
        
        LeanTween.scale(Stars[0], new Vector3(1f, 1f, 1f), 1f).setDelay(2.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Stars[1], new Vector3(1f, 1f, 1f), 1f).setDelay(2.75f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Stars[2], new Vector3(1f, 1f, 1f), 1f).setDelay(3.0f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(Button3_RewardAd, new Vector3(1f, 1f, 1f), 0.5f).setDelay(3.5f).setEase(LeanTweenType.easeOutCirc);        
        LeanTween.scale(Button1, new Vector3(1f, 1f, 1f), 0.5f).setDelay(4.0f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(Button2, new Vector3(1f, 1f, 1f), 0.5f).setDelay(4.0f).setEase(LeanTweenType.easeOutCirc);

        if(SHOWAD>=2)
        {
            PlayerPrefs.SetInt("SHOWAD", 0);
            StartCoroutine(ShowInterstitalAd());
        }
        
    }

    public void UpdateCoinsTxt(int coinAmt)
    {
        txtLevelCoins.text = "+" + coinAmt;
    }

    IEnumerator ShowInterstitalAd()
    {
        interstitialAdManager.LoadAd();
        yield return new WaitForSeconds(4.0f);
        interstitialAdManager.ShowAd();
    }

}
