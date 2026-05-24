using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private float _startingVolume = 2.5f;
    private float _prevSceneVolume;

    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);

        ChangeVolume(_startingVolume);
        _prevSceneVolume = _startingVolume; //To prevent volume immediately being set to 0 in OnEnable
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ReapplyVolume;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReapplyVolume;
    }

    private void ReapplyVolume(Scene scene, LoadSceneMode mode)
    {
        AudioListener.volume = _prevSceneVolume;
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void Mute()
    {
        ChangeVolume(0f);
    }
    public void Unmute()
    {
        ChangeVolume(2.5f);
    }

    public void GetPreviousVolume(float vol)    //Called before scene changes to have AudioListener volume match previous scene
    {
        _prevSceneVolume = vol;
    }
}
