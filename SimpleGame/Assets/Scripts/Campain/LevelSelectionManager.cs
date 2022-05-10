using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{

    [SerializeField] Button[] Levels;
    private int totalLevels, LEVELUNLOCKED;
    private int[] LEVELUNLOCKEDSTARS;

    void Start()
    {
        totalLevels = Levels.Length;
        LEVELUNLOCKEDSTARS = new int[totalLevels];

        GetLevelCompleteStatus();
        UnlockLevels();

        for (int i = 0; i < totalLevels; i++)
        {
            Levels[i].GetComponent<LevelButton>().UpdateLevelButton(i, LEVELUNLOCKEDSTARS[i]);
        }
    }


    private void GetLevelCompleteStatus()
    {
        LEVELUNLOCKED = PlayerPrefs.GetInt("LEVELUNLOCKED", 0);

        for (int i = 0; i < totalLevels; i++)
        {
            LEVELUNLOCKEDSTARS[i] = PlayerPrefs.GetInt("LEVELUNLOCKEDSTARS"+i, 0);
            Debug.Log("Level selection no " + i + " stars unlocked " + LEVELUNLOCKEDSTARS[i]);
        }
    }

    private void UnlockLevels()
    {
        for (int i = 0; i <= LEVELUNLOCKED+1; i++)
        {
            if(i<totalLevels)
            Levels[i].interactable = true;
        }
    }
}
