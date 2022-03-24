using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ReadyPanel : MonoBehaviour
{
    [SerializeField] GameManager gameMgr;

    public void StartGame()
    {
        gameObject.SetActive(false);
        gameMgr.StartGame();
    }
}
