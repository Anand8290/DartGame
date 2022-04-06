using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DartCardDisplay : MonoBehaviour
{
    [SerializeField] DartDB dartDB;
    public int dartIndex;
    private Dart dart;
    private int totalDarts;
    private int defaultDartIndex = 0;
    private int maxSpeed = 15;
    public int price;
    
    [HideInInspector]
    public int speed;
    [HideInInspector]
    public Sprite sprite;

    [Header ("Card Setup")]
    [SerializeField] Image cardDartImage;
    [SerializeField] Image cardDartSpeedImage;
    [SerializeField] Text cardDartSpeed;
    [SerializeField] Text cardDartPrice;

    [SerializeField] GameObject SelectButton, BuyButton;


    void Awake()
    {
        totalDarts = dartDB.totalDarts;
    }

    void Start()
    {
        dartIndex = defaultDartIndex;
        LoadDartFromDB(dartIndex);
        CheckUnlockStatus();
    }

    public void LoadDartFromDB(int dartIndex)
    {
        if(dartIndex < totalDarts)
        {
            dart = dartDB.GetDart(dartIndex);
        }
        
        speed = dart.speed;
        sprite = dart.sprite;
        price = dart.price;

        cardDartImage.sprite = sprite;
        cardDartSpeed.text = speed.ToString() + " / " + maxSpeed.ToString();
        float fillAmt = (float) speed / (float) maxSpeed;
        cardDartSpeedImage.fillAmount = fillAmt;
        cardDartPrice.text = price.ToString();
    }

    public void GoNext()
    {
        if(dartIndex < totalDarts - 1)
        {
            dartIndex ++;
        }
        else
        {
            dartIndex = 0;
        }
        
        LoadDartFromDB(dartIndex);
        CheckUnlockStatus();
    }

    public void GoPrevious()
    {
        if(dartIndex == 0)
        {
            dartIndex = totalDarts - 1;
        }
        else
        {
            dartIndex--;
        }
        
        LoadDartFromDB(dartIndex);
        CheckUnlockStatus();
    }

    public void Select()
    {
        PlayerPrefs.SetInt("DART", dartIndex);
        SceneManager.LoadScene(0);
    }

    public void CheckUnlockStatus()
    {
        int isDartUnlocked = PlayerPrefs.GetInt("DART" + dartIndex, 0);

        if(isDartUnlocked == 1)
        {
            SelectButton.SetActive(true);
            BuyButton.SetActive(false);
        }
        else
        {
            SelectButton.SetActive(false);
            BuyButton.SetActive(true);
        }
    }
}
