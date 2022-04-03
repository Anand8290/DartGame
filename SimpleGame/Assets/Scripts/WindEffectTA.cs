using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindEffectTA : MonoBehaviour
{
    [SerializeField] float minWindSpeed = 0.0f, maxWindSpeed = 1.0f;
    [SerializeField] Text txtWind;

    [HideInInspector]
    public float windSpeed;

    void Start()
    {
        CreateRandomWind();
    }
    
    public void CreateRandomWind()
    {
        windSpeed = Random.Range(minWindSpeed, maxWindSpeed) * IsWind();
        txtWind.text = windSpeed.ToString("F1");
    }

    private int IsWind()
    {
        if (Random.value >= 0.5)
        {
            return 1;
        }
        return 0;
    }  
}
