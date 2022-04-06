using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] Text txtCoins;
    [SerializeField] DartCardDisplay dartCardDisplay;
    private int totalCoins;

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
            UpdateCoinsTxt();
            dartCardDisplay.CheckUnlockStatus();
        }
        else
        {
            Debug.Log("NOT ENOUGH COINS");
        }
    }

    private void UpdateCoinsTxt()
    {
        txtCoins.text = totalCoins.ToString();
    }
}
