using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DartScoreMgr : MonoBehaviour
{
    private int totalDarts;
    public GameObject UIDartScorePrefab;
    public PlayerC playerScript;
    private Text[] txtDartScore;
    
    void Start()
    {
        totalDarts = playerScript.totalDarts;
        txtDartScore = new Text[totalDarts];

        GameObject DS;
        
        for (int i = 0; i < totalDarts; i++)
        {
            DS = Instantiate(UIDartScorePrefab, transform);
            txtDartScore[i] = DS.transform.GetChild(0).GetComponent<Text>();
            DS.name = "DS"+i;
        }
    }

    public void UpdateDartScore(int i, float scoreAmt)
    {
        txtDartScore[i].text = scoreAmt.ToString("F1");
        txtDartScore[i].color = Color.black;
        txtDartScore[i].GetComponentInParent<Image>().color = Color.yellow;
    }
}
