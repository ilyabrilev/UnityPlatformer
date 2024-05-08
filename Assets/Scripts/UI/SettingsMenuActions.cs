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

    void OnEnable()
    {
        LoadVolume();
    }

    public void UpdateMusicVolume(float volume)
    {
        MusicManager.instance.UpdateVolume(volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        SFXManager.instance.UpdateVolume(volume);
    }

    public void SaveVolume()
    {
        MusicManager.instance.SaveVolume();
        SFXManager.instance.SaveVolume();
    }

    public void LoadVolume()
    {
        float musicVolume = MusicManager.instance.GetVolume();
        musicSlider.value = musicVolume;

        float sfxVolume = SFXManager.instance.GetVolume();
        sfxSlider.value = sfxVolume;
    }

    public void Back()
    {
        SFXManager.instance.PlaySound(interactSound);
        SaveVolume();
        currentMenu.SetActive(false);
        previousMenu.SetActive(true);
    }
}
