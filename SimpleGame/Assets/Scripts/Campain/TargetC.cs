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
    private float randomSpeed = 1, randomHeight = 3.5f;
    [SerializeField] float minHeight = 1.0f, maxHeight = 2.75f, minSpeed = 0.5f, maxSpeed = 2.0f;
    float ScreenEdge, speedAdjustForScreen;
    private bool stopMoving = true;
    private float refScreenBoundX = 2.307692f; //Reference value of 1080p resolution oneplus 6T
    [SerializeField] AppreciateManager appreciateMgr;
    [SerializeField] GameObject PopupScore;
    private Animator animator;
    [SerializeField] GameObject DartHit;
    [SerializeField] private Rigidbody2D[] brokenPieces;
    
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
        GameEvents.current.WinGame += OnWinGame;
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        if(!stopMoving)
        {
          OscillateRandom(randomSpeed);
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
            randomSpeed = Random.Range(minSpeed, maxSpeed);
        }
       
        if (transform.position.x >= ScreenEdge)
        {
            direction = -1;
            randomHeight = Random.Range(minHeight, maxHeight);
            transform.position = new Vector3(transform.position.x, randomHeight, 0);
            randomSpeed = Random.Range(minSpeed, maxSpeed);
        }        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float hitPos;

        if(other.gameObject.tag == "dart")
        {
            
            if(DartHit.transform.childCount == 1)
            {
                Destroy(DartHit.transform.GetChild(0).gameObject);
            }
            other.gameObject.transform.SetParent(DartHit.transform);
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
            AudioManager.instance.PlaySound("Hit");

            
        }
    }

    // Game Event functions
    private void StartGame()
    {
        stopMoving = false;
        GameEvents.current.OnStopGame -= StartGame;
    }

    private void StopGame()
    {
        GameEvents.current.OnStopGame -= StopGame;
        stopMoving = true;
        gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        stopMoving = true;
        GameEvents.current.OnPauseGame -= PauseGame;
    }

    private void ResumeGame()
    {
        stopMoving = false;
        GameEvents.current.OnPauseGame += PauseGame;
    }

    private void OnWinGame()
    {
        StartCoroutine(PlayBrokenEffect());
        stopMoving = true;
        DartHit.SetActive(false);
    }

    private IEnumerator PlayBrokenEffect()
    {
        yield return new WaitForFixedUpdate();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
 
            //Enable breakable pieces
            foreach (Rigidbody2D o in brokenPieces)
            {
                o.gameObject.SetActive(true);
                o.transform.parent = null;
                Vector3 forceDirection = (o.transform.position - transform.position).normalized * 7f;
                forceDirection.y = forceDirection.y < 0 ? forceDirection.y * -1 : forceDirection.y;
                o.AddForceAtPosition(forceDirection, transform.position, ForceMode2D.Impulse);
                o.AddTorque(20, ForceMode2D.Impulse);
                Destroy(o.gameObject, 1.5f);
                yield return null;
            }
    } 

}
