using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Text levelNumber;
    [SerializeField] GameObject[] Stars;
    [SerializeField] Button button;
    
    public void UpdateLevelButton(int levelNo, int starsUnlocked)
    {
        levelNumber.text = (levelNo+1).ToString();

        for (int i = 0; i < starsUnlocked; i++)
        {
            Stars[i].SetActive(true);
        }

        button.onClick.AddListener(() => LoadLevel(levelNo));
    }

    private void LoadLevel(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level"+sceneID);
    }
}
