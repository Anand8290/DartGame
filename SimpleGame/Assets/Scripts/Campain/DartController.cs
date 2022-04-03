using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    
    [SerializeField] float speed = 1f;
    float windSpeed = -1f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.up * speed;
        //rb.velocity = new Vector2(windSpeed, speed);
        //Fly(windSpeed);
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

}


