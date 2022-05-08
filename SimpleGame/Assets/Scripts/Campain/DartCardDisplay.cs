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
    public string dartName;

    [HideInInspector]
    public int speed;
    [HideInInspector]
    public Sprite sprite;
    

    [Header ("Card Setup")]
    [SerializeField] Image cardDartImage;
    [SerializeField] Image cardDartSpeedImage;
    [SerializeField] Text cardDartSpeed;
    [SerializeField] Text cardDartPrice;
    [SerializeField] Text cardDartName;

    [SerializeField] GameObject SelectButton, BuyButton;
    [SerializeField] GameObject PriceDisplay;


    void Awake()
    {
        totalDarts = dartDB.totalDarts;
        defaultDartIndex = PlayerPrefs.GetInt("DART", 0);
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
        dartName = dart.name;

        cardDartImage.sprite = sprite;
        cardDartSpeed.text = speed.ToString();
        float fillAmt = (float) speed / (float) maxSpeed;
        cardDartSpeedImage.fillAmount = fillAmt;
        cardDartPrice.text = price.ToString();
        cardDartName.text = dartName;
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
            PriceDisplay.SetActive(false);
        }
        else
        {
            SelectButton.SetActive(false);
            BuyButton.SetActive(true);
            PriceDisplay.SetActive(true);
        }
    }
}
