using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel, Header, Coins, Button1, Button2;
    [SerializeField] Text txtResult, txtLevelCoins;
    [SerializeField] GameObject[] Stars, StarsFill;

    float d;
    
    void Start()
    {
        //Animate();
    }

    public void Animate(bool winGame, int star, int levelCoins)
    {
        Panel.SetActive(true);

        if(winGame)
        {
            txtResult.text = "WINNER";
        }
        else
        {
            txtResult.text = "TRY AGAIN";
        }

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
        LeanTween.alpha(Coins.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.moveLocal(Panel, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(Header, new Vector3(1f, 1f, 1f), 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(Coins.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(2.5f);
        
        LeanTween.scale(Stars[0], new Vector3(1f, 1f, 1f), 1f).setDelay(3.0f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Stars[1], new Vector3(1f, 1f, 1f), 1f).setDelay(3.25f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Stars[2], new Vector3(1f, 1f, 1f), 1f).setDelay(3.5f).setEase(LeanTweenType.easeOutElastic);
                
        LeanTween.scale(Button1, new Vector3(1f, 1f, 1f), 0.5f).setDelay(4.5f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(Button2, new Vector3(1f, 1f, 1f), 0.5f).setDelay(4.5f).setEase(LeanTweenType.easeOutCirc);
    }


}
