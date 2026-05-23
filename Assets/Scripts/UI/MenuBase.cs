using System;
using System.Collections;
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

    public virtual IEnumerator DisableAllMenus()
    {
        if (fog != null) yield return StartCoroutine(Transitions.FadeOut(fog, transitionDuration));
        DisableAllMenusFunc();
        yield return null;
    }

    protected virtual IEnumerator MainMenuActive()  //Enables main menu for the respective scene
    {
        yield return StartCoroutine(DisableAllMenus());
        MainMenu.SetActive(true);
    }

    protected virtual IEnumerator SettingsMenuActive()   //Enables settings menu
    {
        yield return StartCoroutine(DisableAllMenus());
        SettingsMenu.SetActive(true);
    }

    protected virtual IEnumerator AudioMenuActive()    //Enables audio menu
    {
        yield return StartCoroutine(DisableAllMenus());
        AudioMenu.SetActive(true);
    }

    protected virtual IEnumerator LanguageMenuActive()     //Enables language menu
    {
        yield return StartCoroutine(DisableAllMenus());
        LanguageMenu.SetActive(true);
    }

    protected virtual IEnumerator ControlsMenuActive()   //Enables controls menu
    {
        yield return StartCoroutine(DisableAllMenus());
        ControlsMenu.SetActive(true);
    }

    protected virtual IEnumerator ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        yield return null;
    }

    public void GoToMainMenu()
    {
        StartCoroutine(MainMenuActive());
    }

    public void GoToSettings()
    {
        StartCoroutine(SettingsMenuActive());
    }

    public void GoToAudioMenu()
    {
        StartCoroutine(AudioMenuActive());
    }

    public void AudioToggle()
    {
        if (AudioListener.volume > 0)
        {
            AudioIcon.GetComponent<Image>().sprite = AudioMute;
            AudioManager.Instance.Mute();
        }
        else
        {
            AudioIcon.GetComponent<Image>().sprite = AudioUnmute;
            AudioManager.Instance.Unmute();
        }
    }

    public void GoToLanguages()
    {
        StartCoroutine(LanguageMenuActive());
    }

    public void SetLanguageEnglish()
    {
        Debug.Log("Changed locale to English");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
    }
    public void SetLanguageSpanish()
    {
        Debug.Log("Changed locale to Spanish");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("es");
    }

    public void GoToControls()
    {
        StartCoroutine(ControlsMenuActive());
    }

    public void Exit()
    {
        StartCoroutine(ExitGame());
    }


        
    //start a menu transition
    protected IEnumerator MenuTransition(float duration, Action func)
    {
        if (fog != null)
        {
            float halfDuration = duration / 2;
            yield return StartCoroutine(Transitions.FadeOut(fog, halfDuration));
            func();
            yield return StartCoroutine(Transitions.FadeIn(fog, halfDuration));
        }
    }
}
