using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTA : MonoBehaviour
{
    private int direction = 1;
    public GameObject Player;
    private float score, targetLength = 0.625f;
    private float changeSpeedTime, changeSpeedTimeInterval = 5f;
    private float randomSpeed = 1, randomHeight = 3.5f;
    [SerializeField] float minHeight = 1.0f, maxHeight = 2.75f, minSpeed = 0.5f, maxSpeed = 2.0f;
    float ScreenEdge, speedAdjustForScreen;
    public bool stopMoving = false;
    private float refScreenBoundX = 2.307692f; //Reference value of 1080p resolution oneplus 6T
    [SerializeField] AppreciateManager appreciateMgr;
    
    void Awake()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float targetHalfSize = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
        targetLength = (GetComponent<BoxCollider2D>().bounds.size.x / transform.localScale.x)/2;
        ScreenEdge = screenBounds.x - targetHalfSize;
        speedAdjustForScreen = screenBounds.x / refScreenBoundX;
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
        
        transform.Translate(Time.deltaTime * speedAmt * speedAdjustForScreen * direction, 0, 0);
        
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
            other.gameObject.GetComponent<DartControlTA>().HitTarget();
            hitPos = Mathf.Abs(other.gameObject.transform.localPosition.x);
            hitPos = Mathf.Clamp(hitPos, 0, targetLength);
            score = 10 * (targetLength - hitPos)/targetLength;
            
            if(score>=9.9f)
            {
                appreciateMgr.Appreciate(1);
            }
            else if(score>=8.5f)
            {
                appreciateMgr.Appreciate(2);
            }
            
            Player.GetComponent<PlayerTA>().UpdateScore(score);
        }
    }

    
}
