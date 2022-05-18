using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] Text txtCoins;
    [SerializeField] DartCardDisplay dartCardDisplay;
    private int totalCoins;
    [SerializeField] GameObject Message;

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("COINS", 0);
        UpdateCoinsTxt();
        PlayerPrefs.SetInt("DART0", 1);
    }

    public void Buy()
    {
        int price = dartCardDisplay.price;
        int dartIndex = dartCardDisplay.dartIndex;

        if( price <= totalCoins)
        {
            totalCoins -= price;
            PlayerPrefs.SetInt("DART" + dartIndex, 1);
            PlayerPrefs.SetInt("COINS", totalCoins);
            UpdateCoinsTxt();
            dartCardDisplay.CheckUnlockStatus();
        }
        else
        {
            StartCoroutine(MessageUpdate());
        }
    }

    private void UpdateCoinsTxt()
    {
        txtCoins.text = totalCoins.ToString();
    }

    IEnumerator MessageUpdate()
    {
        Message.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Message.SetActive(false);
    }
}
