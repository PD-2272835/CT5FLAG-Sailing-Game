using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;

    public GameObject AudioIcon;

    private void OnEnable()
    {
        GameStateManager.OnPauseGame += OnPause;
    }
    private void OnDisable()
    {
        GameStateManager.OnPauseGame -= OnPause;
    }

    private void Awake()
    {
        CloseMenus();
    }

    private void OnPause(bool gamePauseState)
    {
        if (gamePauseState == true)
        {
            PauseMenuActive();
        }
        else
        {
            CloseMenus();
        }
    }

    private void CloseMenus()   //Disables all menus
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    private void PauseMenuActive()  //Enables pause menu
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }
    private void SettingsMenuActive()   //Enables settings menu
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }
    private void CreditsMenuActive()    //Enables credits menu
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        GameStateManager.Instance.SetPause();
    }

    public void RestartButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();   //Should resume, as game should already be paused to access this button
        SceneManager.LoadScene(currentScene.name);
    }

    public void SettingsButton()
    {
        SettingsMenuActive();
    }

    public void CreditsButton()
    {
        CreditsMenuActive();
    }

    public void SetEnglishButton()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en");
    }
    public void SetSpanishButton()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("es");
    }

    public void AudioMuteButton()
    {
        if (AudioManager.Instance.Muted == false)
        {
            //change audio icon to muted
            AudioManager.Instance.Mute();
        }
        else
        {
            //change audio icon the unmuted
            AudioManager.Instance.Unmute();
        }
    }
}
