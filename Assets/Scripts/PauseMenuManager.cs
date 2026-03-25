using UnityEngine;

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
        GameStateManager.Instance.SetPause(false);
    }

    public void RestartButton()
    {
        ///call reload gamestate & scene
        
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
        //if (AudioManager.Instance.Muted == false)
        //{
        //    //change audio icon to muted
        //    AudioManager.Instance.ChangeVolume(0f);
        //}
        //else
        //{
        //    //change audio icon the unmuted
        //    AudioManager.Instance.ChangeVolume(1f);
        //}
    }

    void Update()
    {
        
    }
}
