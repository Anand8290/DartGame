using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetC : MonoBehaviour
{
    private int direction = 1;
    public GameObject Player;
    private int score;
    private float targetLength = 0.625f;
    private float changeSpeedTime, changeSpeedTimeInterval = 5f;
    private float randomSpeed = 1, randomHeight = 3.5f;
    [SerializeField] float minHeight = 1.0f, maxHeight = 2.75f, minSpeed = 0.5f, maxSpeed = 2.0f;
    float ScreenEdge, speedAdjustForScreen;
    private bool stopMoving = false;
    private float refScreenBoundX = 2.307692f; //Reference value of 1080p resolution oneplus 6T
    [SerializeField] AppreciateManager appreciateMgr;
    [SerializeField] GameObject PopupScore;
    private Animator animator; 
    
    void Awake()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float targetHalfSize = GetComponent<BoxCollider2D>().bounds.size.x * 0.5f;
        targetLength = (GetComponent<BoxCollider2D>().bounds.size.x / transform.localScale.x)/2;
        ScreenEdge = screenBounds.x - targetHalfSize;
        speedAdjustForScreen = screenBounds.x / refScreenBoundX;
        
        // Subscribe to Game Evenets
        GameEvents.current.OnStartGame += StartGame;
        GameEvents.current.OnStopGame += StopGame;
        GameEvents.current.OnPauseGame += PauseGame;
        GameEvents.current.OnResumeGame += ResumeGame;
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*void Update()
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
    }*/

    void FixedUpdate()
    {
        if(!stopMoving)
        {
        if(Time.fixedTime < changeSpeedTimeInterval)
        {  
          OscillateRandom(randomSpeed);
        }
        else
        {
            changeSpeedTimeInterval = Time.fixedTime + Random.Range(2, 6f);
            randomSpeed = Random.Range(minSpeed, maxSpeed);
        }
        }
    }



    private void OscillateRandom(float speedAmt)
    {
        
        transform.Translate(Time.fixedDeltaTime * speedAmt * speedAdjustForScreen * direction, 0, 0);
        
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
            score = Mathf.RoundToInt(10 * (targetLength - hitPos)/targetLength);
            
            if(score==10)
            {
                appreciateMgr.Appreciate(1);
            }
            else if(score==9)
            {
                appreciateMgr.Appreciate(2);
            }
            
            PopupScore.SetActive(true);
            PopupScore.GetComponent<Text>().text = score.ToString();
            PopupScore.GetComponent<PopupScore>().Animate();
            Player.GetComponent<PlayerC>().UpdateScore(score);

            animator.SetTrigger("Hit");
        }
    }

    // Game Event functions
    private void StartGame()
    {
        stopMoving = false;
    }

    private void StopGame()
    {
        stopMoving = true;
        gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        stopMoving = true;
    }

    private void ResumeGame()
    {
        stopMoving = false;
    }
}
