using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    public Sound[] musicSounds, sfxSounds;
    //link Audiosources that will play music and/or sound effects
    public AudioSource musicSource, sfxSource;

    public float musicV = 100;
    public float SFXV = 100;
    // Start is called before the first frame update
    private void Awake()
    {
       if (instance == null)
       {
            instance = this;
            DontDestroyOnLoad(gameObject);
       }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    void Start()
    {
        MusicVolume();
        SFXVolume();
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayMusic(string name)
    {
        //finds the song name
        Sound s = Array.Find(musicSounds, x => x.name == name);
        //if song name could not be found
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            //play music clip
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        //finds the sfx name
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        //if sfx name could not be found
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            //play sfx clip
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
    public void ToggleMusic()
    {

        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume()
    {
        musicSource.volume = musicV;
    }
    public void SFXVolume()
    {
        sfxSource.volume = SFXV;
    }
}
