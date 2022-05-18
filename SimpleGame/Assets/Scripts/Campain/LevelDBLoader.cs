using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDBLoader : MonoBehaviour
{

    [SerializeField] LevelDB levelDB;
    public int levelIndex;
    private Level currentLevel;
    private int totalLevels;
    
    [HideInInspector]
    public int score, darts, rewardCoins;

    void Awake()
    {
        totalLevels = levelDB.totalLevels;

        if(levelIndex<totalLevels)
        {
            currentLevel = levelDB.GetLevel(levelIndex);
        }
        
        score = currentLevel.score;
        darts = currentLevel.darts;
        rewardCoins = currentLevel.rewardCoins;
    }   
}
