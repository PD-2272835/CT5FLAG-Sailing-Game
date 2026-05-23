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

    protected Image fog;

    protected virtual void MainMenuActive()  //Enables main menu for the respective scene
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    protected virtual void SettingsMenuActive()   //Enables settings menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    protected virtual void AudioMenuActive()    //Enables audio menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(true);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    protected virtual void LanguageMenuActive()     //Enables language menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(true);
        ControlsMenu.SetActive(false);
    }

    protected virtual void ControlsMenuActive()   //Enables controls menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    protected virtual void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        MainMenuActive();
    }

    public void GoToSettings()
    {
        SettingsMenuActive();
    }

    public void GoToAudioMenu()
    {
        AudioMenuActive();
    }

    public void AudioToggle ()
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
        LanguageMenuActive();
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
        ControlsMenuActive();
    }

    public void Exit()
    {
        ExitGame();
    }
}
