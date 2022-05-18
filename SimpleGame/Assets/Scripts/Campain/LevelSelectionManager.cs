using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{

    [Header("Levels")]
    [SerializeField] Button[] Levels;
    private int totalLevels, LEVELUNLOCKED;
    private int[] LEVELUNLOCKEDSTARS;

    [Header("Managers")]
    [SerializeField] GpgsAchievement gAchievement;

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

        CheckUnlockAchievement();
    }


    private void GetLevelCompleteStatus()
    {
        LEVELUNLOCKED = PlayerPrefs.GetInt("LEVELUNLOCKED", -1);

        for (int i = 0; i < totalLevels; i++)
        {
            LEVELUNLOCKEDSTARS[i] = PlayerPrefs.GetInt("LEVELUNLOCKEDSTARS"+i, 0);
        }
    }

    private void UnlockLevels()
    {
        Levels[0].interactable = true;

        for (int i = 1; i <= LEVELUNLOCKED+1; i++)
        {
            if(i<totalLevels)
            Levels[i].interactable = true;
        }
    }

    private void CheckUnlockAchievement()
    {
        if(LEVELUNLOCKED >= 0)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_babyface);
        }

        if(LEVELUNLOCKED >= 4)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_little_john);
        }

        if(LEVELUNLOCKED >= 9)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_gentle_giant);
        }

        if(LEVELUNLOCKED >= 19)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_the_magnet);
        }

        if(LEVELUNLOCKED >= 29)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_the_cowboy);
        }

        if(LEVELUNLOCKED >= 39)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_the_king);
        }

        if(LEVELUNLOCKED >= 49)
        {
            gAchievement.UnlockAcheivement(GPGSIds.achievement_superman);
        }
    }
}
