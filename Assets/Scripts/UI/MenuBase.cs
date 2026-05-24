using System;
using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject AudioMenu;
    public GameObject LanguageMenu;
    public GameObject ControlsMenu;

    public GameObject AudioIcon;
    public Sprite AudioMute;
    public Sprite AudioUnmute;
    public GameObject VolumeSlider;

    protected float transitionDuration = 1f; // how much time a menu transition should take (this should be set by each menu manager)
    [SerializeField] protected Image fog; //fading

    public void DisableAllMenusFunc()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void PlayUISound()
    {
        GetComponent<AudioSource>().Play();
    }

    public virtual IEnumerator DisableAllMenus()
    {
        if (fog != null) yield return StartCoroutine(Transitions.FadeOut(fog, transitionDuration));
        DisableAllMenusFunc();
        yield return null;
    }

    protected virtual IEnumerator MainMenuActive()  //Enables main menu for the respective scene
    {
        MainMenu.SetActive(true);
        yield return null;
    }

    protected virtual IEnumerator SettingsMenuActive()   //Enables settings menu
    {
        SettingsMenu.SetActive(true);
        yield return null;
    }

    protected virtual IEnumerator AudioMenuActive()    //Enables audio menu
    {
        AudioMenu.SetActive(true);
        yield return null;
    }

    protected virtual IEnumerator LanguageMenuActive()     //Enables language menu
    {
        LanguageMenu.SetActive(true);
        yield return null;
    }

    protected virtual IEnumerator ControlsMenuActive()   //Enables controls menu
    {
        ControlsMenu.SetActive(true);
        yield return null;
    }

    protected virtual IEnumerator ExitGame()
    {
        StartCoroutine(Transitions.FadeOut(fog, 2f));
        Debug.Log("ExitGame");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        yield return null;
    }

    public virtual void GoToMainMenu()
    {
        PlayUISound();

        StartCoroutine(MainMenuActive());
    }

    public virtual void GoToSettings()
    {
        PlayUISound();

        StartCoroutine(SettingsMenuActive());
    }

    public virtual void GoToAudioMenu()
    {
        PlayUISound();

        DisableAllMenusFunc();
        StartCoroutine(AudioMenuActive());
    }

    public virtual void GoToLanguages()
    {
        PlayUISound();

        DisableAllMenusFunc();
        StartCoroutine(LanguageMenuActive());
    }

    public virtual void GoToControls()
    {
        PlayUISound();

        StartCoroutine(ControlsMenuActive());
    }

    public virtual void Exit()
    {
        PlayUISound();

        StartCoroutine(ExitGame());
    }



    public void AudioToggle()
    {
        PlayUISound();

        if (AudioListener.volume > 0)
        {
            AudioIcon.GetComponent<Image>().sprite = AudioMute;
            VolumeSlider.GetComponent<Slider>().value = 0f;
            AudioManager.Instance.Mute();
        }
        else
        {
            AudioIcon.GetComponent<Image>().sprite = AudioUnmute;
            VolumeSlider.GetComponent<Slider>().value = 2.5f;
            AudioManager.Instance.Unmute();
        }
    }

    public void SetLanguageEnglish()
    {
        PlayUISound();

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
        Debug.Log("Changed locale to English");
    }
    public void SetLanguageSpanish()
    {
        PlayUISound();

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("es");
        Debug.Log("Changed locale to Spanish");
    }

}
