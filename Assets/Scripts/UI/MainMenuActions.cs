using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private GameObject thisMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;
    public void PlayClick()
    {
        SFXManager.instance.PlaySound(interactSound);
        //MusicManager.instance.PlayMusic(null, 0, 2);
        SceneManager.LoadScene(1);
    }

    public void SettingsClick()
    {
        SFXManager.instance.PlaySound(interactSound);
        thisMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CreditsClick()
    {
        SFXManager.instance.PlaySound(interactSound);
    }

    public void QuitClick()
    {
        SFXManager.instance.PlaySound(interactSound);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
