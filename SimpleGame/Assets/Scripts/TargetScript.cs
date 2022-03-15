using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    //public float horMoveSpeed;
    private int direction = 1;
    public GameObject Player;
    private float score, targetLength;
    private float changeSpeedTime, changeSpeedTimeInterval = 5f;
    private float randomSpeed = 1, randomHeight = 3.5f;
    public bool stopMoving = false;

    void Start()
    {
        targetLength = 0.5f;        
    }


    void Update()
    {
        if(!stopMoving)
        {
        if(Time.time < changeSpeedTimeInterval)
        {  
          OscillateRandom(randomSpeed);
        }
        else
        {
            changeSpeedTimeInterval = Time.time + Random.Range(2, 6f);
            randomSpeed = Random.Range(0.5f, 2f);
            randomHeight = Random.Range(1.0f, 3.5f);
            transform.position = new Vector3(transform.position.x, randomHeight, 0);
        }
        }
        
    }

    /*private void Oscillate()
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
    }*/

    private void OscillateRandom(float speedAmt)
    {
        
        transform.Translate(Time.deltaTime * speedAmt * direction, 0, 0);
        
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
            if(transform.childCount == 1)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            other.gameObject.GetComponent<DartController>().HitTarget();
            other.gameObject.transform.SetParent(this.gameObject.transform);
            hitPos = Mathf.Abs(other.gameObject.transform.localPosition.x);
            hitPos = Mathf.Clamp(hitPos, 0, targetLength);
            score = 10 * (targetLength - hitPos)/targetLength;
            Player.GetComponent<PlayerController>().UpdateScore(score);
        }
    }
}
