using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTA : MonoBehaviour
{
    private int direction = 1;
    public GameObject Player;
    private float score, targetLength = 0.625f;
    private float changeSpeedTime, changeSpeedTimeInterval = 5f;
    private float randomSpeed = 1, randomHeight = 3.5f;
    [SerializeField] float minHeight = 1.0f, maxHeight = 2.75f, ScreenEdge = 1.55f, minSpeed = 0.5f, maxSpeed = 2.0f;
    public bool stopMoving = false;

    void Start()
    {
        targetLength = (GetComponent<BoxCollider2D>().bounds.size.x / transform.localScale.x)/2;
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
            randomSpeed = Random.Range(minSpeed, maxSpeed);
            
        }
        }
        
    }

    private void OscillateRandom(float speedAmt)
    {
        
        transform.Translate(Time.deltaTime * speedAmt * direction, 0, 0);
        
        if(transform.position.x <= -ScreenEdge)
        {
            direction = 1;
            randomHeight = Random.Range(minHeight, maxHeight);
            transform.position = new Vector3(transform.position.x, randomHeight, 0);
        }
       
        if (transform.position.x >= ScreenEdge)
        {
            direction = -1;
            randomHeight = Random.Range(minHeight, maxHeight);
            transform.position = new Vector3(transform.position.x, randomHeight, 0);
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
            other.gameObject.transform.SetParent(this.gameObject.transform);
            other.gameObject.GetComponent<DartController>().HitTarget();
            hitPos = Mathf.Abs(other.gameObject.transform.localPosition.x);
            hitPos = Mathf.Clamp(hitPos, 0, targetLength);
            score = 10 * (targetLength - hitPos)/targetLength;
            Player.GetComponent<PlayerTA>().UpdateScore(score);
        }
    }
}
