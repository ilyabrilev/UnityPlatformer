using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuActions : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;

    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Menu Hierarchy")]
    [SerializeField] private GameObject previousMenu;
    [SerializeField] private GameObject currentMenu;

    private void Start()
    {
        LoadVolume();
    }

    public void UpdateMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        mixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        mixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicSlider.value = musicVolume;
        UpdateMusicVolume(musicVolume);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        sfxSlider.value = sfxVolume;
        UpdateSFXVolume(sfxVolume);
    }

    public void Back()
    {
        SFXManager.instance.PlaySound(interactSound);
        SaveVolume();
        currentMenu.SetActive(false);
        previousMenu.SetActive(true);
    }
}
