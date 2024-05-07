using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        //Keep this object 
        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float _change, AudioSource source)
    {
        float currentValue = PlayerPrefs.GetFloat(volumeName, 1);
        currentValue += _change;

        if (currentValue > 1) {
            currentValue = 0;
        } else if (currentValue < 0)
        {
            currentValue = 1;
        }
        currentValue = Mathf.Clamp(currentValue, 0, 1);

        source.volume = currentValue * baseVolume;

        PlayerPrefs.SetFloat(volumeName, currentValue);

    }
}
