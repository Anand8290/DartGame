using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    
    [SerializeField] float speed = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
        //Destroy(this.gameObject, 5f);
    }

    public void HitTarget()
    {
        speed = 0;
        rb.isKinematic = false;
        //GetComponent<BoxCollider2D>().enabled = false;
    }

}


