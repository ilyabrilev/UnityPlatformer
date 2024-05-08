using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    void Start()
    {      
    }

    private void Awake()
    {
        StartCoroutine(StartMenu());
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(2);
        MusicManager.instance.PlayMusic("Menu Theme", 1);
    }
}
