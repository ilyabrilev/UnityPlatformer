using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseActions : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private GameObject thisMenu;
    [SerializeField] private GameObject settingsMenu;

    public void Resume()
    {
        SFXManager.instance.PlaySound(interactSound);
        pauseScreen.OnEscapePress();
    }

    public void Settings()
    {
        SFXManager.instance.PlaySound(interactSound);
        thisMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        SFXManager.instance.PlaySound(interactSound);
        pauseScreen.OnEscapePress();
        MusicManager.instance.PlayMusic("Menu Theme", 1, 0.5f);
        SceneManager.LoadScene("_MainMenu");
    }

    public void Quit()
    {
        SFXManager.instance.PlaySound(interactSound);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
