using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DartDB", menuName = "Scriptable Objects/DartDB")]
public class DartDB : ScriptableObject
{
    public Dart[] Darts;
    
    public int totalDarts
    {
        get
        {
            return Darts.Length;
        }
    }

    public Dart GetDart(int index)
    {
        return Darts[index];
    }
}
