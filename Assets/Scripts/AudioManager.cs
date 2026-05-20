using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public bool Muted;

    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);

        Muted = false;
        ChangeVolume(1f);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;   ///will this persist between scenes?
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
