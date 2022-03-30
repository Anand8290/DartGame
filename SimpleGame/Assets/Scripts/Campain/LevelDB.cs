using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDB", menuName = "Scriptable Objects/LevelDB")]
public class LevelDB : ScriptableObject
{
    public Level[] levels;
    
    public int totalLevels
    {
        get
        {
            return levels.Length;
        }
    }

    public Level GetLevel(int index)
    {
        return levels[index];
    }
}
