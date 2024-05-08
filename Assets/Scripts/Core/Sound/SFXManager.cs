using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : AbstractAudioManager
{
    public static SFXManager instance { get; private set; }
    [SerializeField] private AudioSource soundSource;
    public override string SettingName
    {
        get
        {
            return "SFXVolume";
        }
    }

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
    }

    private void Start()
    {
        LoadVolume();
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
}
