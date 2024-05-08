using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreenObject;
    [SerializeField] private GameObject mainPauseMenu;
    [SerializeField] private GameObject settingsPauseMenu;
    
    public void SetActive(bool active)
    {
        pauseScreenObject.SetActive(active);
    }

    private void PauseGame(bool status)
    {
        pauseScreenObject.SetActive(status);
        if (status)
        {
            mainPauseMenu.SetActive(true);
            settingsPauseMenu.SetActive(false);
        }

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OnEscapePress()
    {
        if (pauseScreenObject.activeInHierarchy)
        {
            PauseGame(false);
        }
        else
        {
            PauseGame(true);
        }        
    }
}
