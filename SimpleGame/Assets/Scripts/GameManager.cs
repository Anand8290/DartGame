using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject GetReadyPanel, gameoverPanel;
    [SerializeField] GameObject buttonHsLB;
    [SerializeField] GameObject player, target;

    void Start()
    {
        GetReadyPanel.SetActive(true);
    }

    public void StartGame()
    {
        player.GetComponent<PlayerTA>().startGame = true;
        target.GetComponent<TargetTA>().stopMoving = false;
    }

    public void GameOver()
    {
        target.GetComponent<TargetTA>().stopMoving = true;
        target.GetComponent<BoxCollider2D>().enabled = false;
        player.SetActive(false);
        target.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    public void ScoredHigh()
    {
        buttonHsLB.SetActive(true);
    }
 
}
