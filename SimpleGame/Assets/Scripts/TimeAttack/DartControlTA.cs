using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartControlTA : MonoBehaviour
{
    
    [SerializeField] float speed = 1f;
    float windSpeed;
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
        //rb.velocity = new Vector2(windSpeed, speed);
        StartCoroutine(NewFly(windSpeed));
    }

    IEnumerator NewFly(float windSpeed)
    {
        rb.velocity = new Vector2(windSpeed, speed * 1.2f);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

}


