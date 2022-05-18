using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;
    [SerializeField] GameObject MenuPanel;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPanel.SetActive(false);
            QuitPanel.SetActive(true);
        }
    }
}
