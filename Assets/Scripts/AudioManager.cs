using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource[] _audioSources;
    public bool Muted;

    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);

        GetAudioSources();

        Muted = false;
        ChangeVolume(1f);
    }

    public void GetAudioSources()
    {
        _audioSources = null;
        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    public void ChangeVolume(float value)
    {
        for(int i = 0;  i < _audioSources.Length; i++)
        {
            _audioSources[i].volume = value;
        }
    }

    public void Mute()
    {
        ChangeVolume(0f);
        Muted = true;
    }
    public void Unmute()
    {
        ChangeVolume(1f);
        Muted = false;
    }
}
