using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioVolumeController[] audios;

    [Range(0, 1)]
    [Tooltip("Maximo volumen permitido")]
    public float maxVolumeLevel;

    [Range(0, 1)]
    [Tooltip("Volumen actual de la musica")]
    public float CurrentAudioVolume;

    [Range(0, 1)]
    [Tooltip("Volumen actual de los efectos")]
    public float CurrentSFXVolume;

    public bool SFXMute;
    public bool AudioMute;

    private static AudioVolumeManager sharedInstance = null;

    public static AudioVolumeManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audios = FindObjectsOfType<AudioVolumeController>();

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            CurrentSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        }

        if (PlayerPrefs.HasKey("AudioVolume"))
        {
            CurrentAudioVolume = PlayerPrefs.GetFloat("AudioVolume");
        }

        if (PlayerPrefs.HasKey("SFXMute"))
        {
            SFXMute = (PlayerPrefs.GetInt("SFXMute") == 1);
        }

        if (PlayerPrefs.HasKey("AudioMute"))
        {
            AudioMute = (PlayerPrefs.GetInt("AudioMute") == 1);
        }

        if (AudioMute)
        {
            ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC, 0);
        }
        else
        {
            ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC, CurrentAudioVolume);
        }

        if (SFXMute)
        {
            ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX, 0);
        }
        else
        {
            ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX, CurrentSFXVolume);
        }
    }

    private void ChangeGlobalAudioVolume(AudioVolumeController.AudioType type, float newVolume)
    {
        if (newVolume >= maxVolumeLevel)
        {
            newVolume = maxVolumeLevel;
        }

        foreach (AudioVolumeController ac in audios)
        {
            if (ac.type == type)
            {
                ac.SetAudioLevel(newVolume);
            }
        }
    }

    public void AudioChanged(Slider audioSlide)
    {
        if (AudioMute) return;
        CurrentAudioVolume = audioSlide.value;
        PlayerPrefs.SetFloat("AudioVolume", CurrentAudioVolume);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC, CurrentAudioVolume);
    }

    public void SFXChanged(Slider audioSlide)
    {
        if (SFXMute) return;
        CurrentSFXVolume = audioSlide.value;
        PlayerPrefs.SetFloat("SFXMute", CurrentSFXVolume);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX, CurrentSFXVolume);
    }

    public void MuteSFX()
    {
        SFXMute = true;
        PlayerPrefs.SetInt("SFXMute", 1);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX, 0);
    }

    public void MuteAudio()
    {
        SFXMute = true;
        PlayerPrefs.SetInt("AudioMute", 1);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC, 0);
    }

    public void UnMuteSFX()
    {
        SFXMute = false;
        PlayerPrefs.SetInt("SFXMute", 0);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.SFX, CurrentSFXVolume);
    }

    public void UnMuteAudio()
    {
        AudioMute = false;
        PlayerPrefs.SetInt("AudioMute", 0);
        ChangeGlobalAudioVolume(AudioVolumeController.AudioType.MUSIC, CurrentAudioVolume);
    }
}
