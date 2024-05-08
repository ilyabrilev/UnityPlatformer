using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header ("Game over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private PauseScreen pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseScreen.OnEscapePress();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SFXManager.instance.PlaySound(gameOverSound);
    }
}
