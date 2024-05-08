using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

abstract public class AbstractAudioManager : MonoBehaviour
{
    [SerializeField] protected AudioMixer mixer;
    protected bool isLoaded = false;

    public abstract string SettingName //SFXVolume || MusicVolume
    {
        get;
    }

    public float GetVolume()
    {
        mixer.GetFloat(SettingName, out float volume);
        return volume;
    }

    public void LoadVolume()
    {
        isLoaded = true;
        float volume = PlayerPrefs.GetFloat(SettingName);
        mixer.SetFloat(SettingName, volume);
    }

    public void UpdateVolume(float volume)
    {
        mixer.SetFloat(SettingName, volume);
    }

    public void SaveVolume()
    {
        mixer.GetFloat(SettingName, out float musicVolume);
        PlayerPrefs.SetFloat(SettingName, musicVolume);
    }
}
