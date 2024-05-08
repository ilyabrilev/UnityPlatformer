using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreenObject;
    [SerializeField] private GameObject mainPauseMenu;
    [SerializeField] private GameObject settingsPauseMenu;
    [SerializeField] private GameObject player;
    
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

        Time.timeScale = status ? 0.0f : 1.0f;
        player.GetComponent<PlayerAttack>().enabled = !status;
        player.GetComponent<PlayerMovement>().enabled = !status;
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
