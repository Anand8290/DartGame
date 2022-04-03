using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] LevelDBLoader levelDBLoader;
    public int totalCoins, levelCoins;
    int maxCoins = 0;

    void Start()
    {
        maxCoins = levelDBLoader.rewardCoins;
    }

    public void AddCoins(int coinAmt)
    {
        totalCoins += coinAmt;
    }

    public void CalculateCoins(int star)
    {
        levelCoins = maxCoins * star/3;
        totalCoins += levelCoins;
    }
}
