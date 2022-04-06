using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartDBLoader : MonoBehaviour
{
    [SerializeField] DartDB dartDB;
    [SerializeField] int dartIndex;
    private Dart dart;
    private int totalDarts;
    
    [HideInInspector]
    public int speed;
    [HideInInspector]
    public Sprite sprite;

    void Awake()
    {
        totalDarts = dartDB.totalDarts;
    }

    public void LoadDartFromDB(int dartIndex)
    {
        if(dartIndex < totalDarts)
        {
            dart = dartDB.GetDart(dartIndex);
        }
        
        speed = dart.speed;
        sprite = dart.sprite;
    }
}
