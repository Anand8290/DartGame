using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindEffectTA : MonoBehaviour
{
    [SerializeField] float minWindSpeed = 0.0f, maxWindSpeed = 1.0f;
    [SerializeField] Text txtWind;
    [SerializeField] Image windDirImg;
    [SerializeField] Sprite windDirLeft, windDirRight, windDirUp;

    [HideInInspector]
    public float windSpeed;

    void Start()
    {
        CreateRandomWind();
    }
    
    public void CreateRandomWind()
    {
        windSpeed = Random.Range(minWindSpeed, maxWindSpeed) * IsWind();
        txtWind.text = Mathf.Abs(windSpeed).ToString("F1");
        UpdateWindDirection();
    }

    private int IsWind()
    {
        if (Random.value >= 0.5)
        {
            return 1;
        }
        return 0;
    }
    
    private void UpdateWindDirection()
    {
        if(windSpeed == 0)
        {
            windDirImg.sprite = windDirUp;
        }
        else if(windSpeed > 0)
        {
            windDirImg.sprite = windDirRight;
        }
        else
        {
            windDirImg.sprite = windDirLeft;
        }
    }   
}
