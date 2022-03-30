using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDBLoader : MonoBehaviour
{

    [SerializeField] LevelDB levelDB;
    [SerializeField] int levelIndex;
    private Level currentLevel;
    private int totalLevels;
    
    [HideInInspector]
    public int score, darts;

    void Awake()
    {
        totalLevels = levelDB.totalLevels;

        if(levelIndex<totalLevels)
        {
            currentLevel = levelDB.GetLevel(levelIndex);
        }
        
        score = currentLevel.score;
        darts = currentLevel.darts;
    }
   
}
