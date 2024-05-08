using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : AbstractAudioManager
{
    public static MusicManager instance { get; private set; }

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    public override string SettingName
    {
        get
        {
            return "MusicVolume";
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

    public void PlayMusic(string trackName, float fadeInDuration = 0.5f, float fadeOutDuration = 0.5f)
    {
        if (!isLoaded)
        {
            LoadVolume();
        }
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeInDuration, fadeOutDuration));
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeInDuration = 0.5f, float fadeOutDuration = 0.5f)
    {
        float percent = 0;
        if (musicSource.clip != null) { 

            if (fadeOutDuration <= 0)
            {
                musicSource.volume = 1;
            } else
            {
                while (percent < 1)
                {
                    percent += Time.deltaTime * 1 / fadeOutDuration;
                    musicSource.volume = Mathf.Lerp(1, 0, percent);
                    yield return null;
                }
            }
        }

        if (nextTrack != null)
        {
            musicSource.volume = 0;
            musicSource.clip = nextTrack;
            musicSource.Play();

            if (fadeInDuration <= 0)
            {
                musicSource.volume = 1;
            }
            else
            {   
                percent = 0;
                while (percent < 1)
                {
                    percent += Time.deltaTime * 1 / fadeInDuration;
                    musicSource.volume = Mathf.Lerp(0, 1, percent);
                    yield return null;
                }
            }            
        }
    }
}
