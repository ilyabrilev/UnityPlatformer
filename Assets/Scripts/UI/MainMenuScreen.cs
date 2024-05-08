using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    void Start()
    {
        //MusicManager.instance.LoadVolume();
        //SFXManager.instance.LoadVolume();
        MusicManager.instance.PlayMusic("Menu Theme", 2);
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(2);
        MusicManager.instance.PlayMusic("Menu Theme", 1);
    }
}
