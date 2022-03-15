using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float horMoveSpeed;
    private int direction = 1;
    public GameObject Player;
    private float score, targetLength;

    void Start()
    {
        targetLength = 0.5f;
    }


    void Update()
    {
        Oscillate();
    }


    private void Oscillate()
    {
        
        transform.Translate(Time.deltaTime * horMoveSpeed * direction, 0, 0);
        //transform.position += new Vector3(Time.deltaTime * horMoveSpeed * direction, 0, 0);
        
        if(transform.position.x <= -1.8f)
        {
            direction = 1;
        }
       
        if (transform.position.x >= 1.8f)
        {
            direction = -1;
        }        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float hitPos;

        if(other.gameObject.tag == "dart")
        {
            other.gameObject.GetComponent<DartController>().HitTarget();
            other.gameObject.transform.SetParent(this.gameObject.transform);
            hitPos = Mathf.Abs(other.gameObject.transform.localPosition.x);
            hitPos = Mathf.Clamp(hitPos, 0, targetLength);
            score = 10 * (targetLength - hitPos)/targetLength;
            Player.GetComponent<PlayerController>().UpdateScore(score);
        }
    }
}
