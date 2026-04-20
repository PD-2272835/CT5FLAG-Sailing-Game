using UnityEngine;
using UnityEngine.SceneManagement;  ///replace if changing restart button

public class PauseMenuManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;

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
    public void OnPause(bool gamePauseState)
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

    private void CloseMenus()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    private void PauseMenuActive()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }
    private void SettingsMenuActive()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void ResumeButton()
    {
        GameStateManager.Instance.SetPause();
    }

    public void RestartButton() ///rework to call function in GameStateManager
    {
        Scene currentScene = SceneManager.GetActiveScene();

        GameStateManager.Instance.SetPause();   //Should resume, as game should already be paused to access this button
        SceneManager.LoadScene(currentScene.name);
    }

    public void SettingsButton()
    {
        SettingsMenuActive();
    }

    public void CreditsButton()
    {
        ///credits asset
    }

    public void SetEnglishButton()
    {
        ///call change text to english
    }
    public void SetJapaneseButton()
    {
        ///call change text to japanese
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

    void Update()
    {
        
    }
}
