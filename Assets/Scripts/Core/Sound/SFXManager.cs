using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; }
    [SerializeField] private AudioSource soundSource;

    private void Awake()
    { 
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
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void PlaySound3D(AudioClip _sound, Vector3 position)
    {
        if (_sound != null)
        {
            AudioSource.PlayClipAtPoint(_sound, position);
        }        
    }

    public void ChangeSoundVolume(float _change)
    {
        //ChangeSourceVolume(1, "soundVolume", _change, soundSource);
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
