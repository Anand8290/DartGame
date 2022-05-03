using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] LevelDBLoader levelDBLoader;
    public int totalCoins, levelCoins;
    int maxCoins = 0;

    [SerializeField] GameOverPanel gameoverpanel;

    void Start()
    {
        maxCoins = levelDBLoader.rewardCoins;
        totalCoins = PlayerPrefs.GetInt("COINS", 0);
    }

    public void AddCoins(int coinAmt)
    {
        totalCoins += coinAmt;
    }

    public void CalculateCoins(int star)
    {
        levelCoins = maxCoins * star/3;
        totalCoins += levelCoins;
        PlayerPrefs.SetInt("COINS", totalCoins);
    }

    public void RewardCoins()
    {
        totalCoins += levelCoins;
        gameoverpanel.UpdateCoinsTxt(levelCoins*2);
        PlayerPrefs.SetInt("COINS", totalCoins);
    }
}
