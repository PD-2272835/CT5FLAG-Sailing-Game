using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBase : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject AudioMenu;
    public GameObject LanguageMenu;

    public GameObject AudioIcon;

    protected virtual void MainMenuActive()  //Enables main menu for the respective scene
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
    }

    protected virtual void SettingsMenuActive()   //Enables settings menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
    }

    protected virtual void AudioMenuActive()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(true);
        LanguageMenu.SetActive(false);
    }

    protected virtual void LanguageMenuActive()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(true);
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

    public void AudioMute()
    {
        AudioManager.Instance.Mute();
    }

    public void AudioUnmute()
    {
        AudioManager.Instance.Unmute();
    }

    public void GoToLanguages()
    {
        LanguageMenuActive();
    }

    public void SetLanguageEnglish()
    {
        ///call change text to english
    }
    public void SetLanguageSpanish()
    {
        ///call change text to spanish
    }

    public void Exit()
    {
        ExitGame();
    }
}
