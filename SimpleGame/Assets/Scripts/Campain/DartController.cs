using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    
    private float speed = 1f;
    private float windSpeed = -1f;
    Rigidbody2D rb;
    DartDBLoader dartDB;
    SpriteRenderer sR;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dartDB = GetComponent<DartDBLoader>();   
        sR = GetComponent<SpriteRenderer>();    
    }

    public void HitTarget()
    {
        speed = 0;
        rb.isKinematic = false;
        GetComponentInChildren<TrailRenderer>().enabled = false;
    }

    public void Fly(float windSpeed)
    {   
        rb.velocity = new Vector2(windSpeed, speed);
    }

    public void UpdateDart(int dartIndex)
    {   
        dartDB.LoadDartFromDB(dartIndex);
        speed = dartDB.speed;
        sR.sprite = dartDB.sprite;
    }

}


