using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] GameObject PausePanelUI;
    [SerializeField] GameObject PauseButton;

    public static bool GameIsPaused = false;
    
    public void PauseRestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(int sceneID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        GameIsPaused = true;
        GameEvents.current.PauseGameEvent();
        PauseButton.SetActive(false);
        PausePanelUI.SetActive(true);
        PausePanelUI.GetComponent<PausePanel>().Animate();
        Time.timeScale = 0;
        
    }

    public void PauseGameTA()
    {
        GameIsPaused = true;
        PauseButton.SetActive(false);
        PausePanelUI.SetActive(true);
        PausePanelUI.GetComponent<PausePanel>().Animate();
        Time.timeScale = 0;
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanelUI.SetActive(false);
        PauseButton.SetActive(true);
        GameIsPaused = false;
        GameEvents.current.ResumeGameEvent();
    }

    public void ResumeGameTA()
    {
        Time.timeScale = 1;
        PausePanelUI.SetActive(false);
        PauseButton.SetActive(true);
        GameIsPaused = false;
    }

    public void OnClickPlayButtonSound()
    {
        AudioManager.instance.PlaySound("Button");
    }

}
