using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private Slider _volumeSlider;

    public static AudioManager Instance { get; private set; }

    [SerializeField] private float _startingVolume = 2.5f;
    private static float _trackedVolume;
    private static bool _isMuted;

    private void Start()
    {
        //get an initial reference to the volume slider - the menu manager in each scene should be tagged accordingly
        _volumeSlider = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuBase>().VolumeSlider;
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _volumeSlider.value = _startingVolume;
    }

    private void Awake()
    {
        //this is a singleton
        if (Instance != this)
        {
            if (Instance == null) { 
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else { Destroy(this); }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _volumeSlider = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuBase>().VolumeSlider;
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _volumeSlider.value = _trackedVolume;
    }

    public void ChangeVolume(float value)
    {
        if (value > 0) _trackedVolume = value;
        AudioListener.volume = value;
    }


    public void Mute()
    {
        _isMuted = true;
        _volumeSlider.value = 0f; //set volume slider value to trigger change volume
    }
    public void Unmute()
    {
        _isMuted = false;
        _volumeSlider.value = _trackedVolume; //set volume slider value to trigger change volume
    }
}
