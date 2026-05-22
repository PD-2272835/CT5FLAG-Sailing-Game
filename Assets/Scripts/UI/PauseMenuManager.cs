using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuBase
{
    public GameObject ControlsMenu; ///should be removed from here and TitleScreen & added to MenuBase

    private void Awake()
    {
        CloseMenus();
    }

    private void OnEnable()
    {
        GameStateManager.OnPauseGame += OnPause;
    }
    private void OnDisable()
    {
        GameStateManager.OnPauseGame -= OnPause;
    }

    private void OnPause(bool gamePauseState)
    {
        if (!GameplayUI.Instance.GameOverBool)  /// temp    /// if (GameStateManager.Instance.CurrentState != EndGame)
        {
            if (gamePauseState == true)
            {
                MainMenuActive();
            }
            else
            {
                CloseMenus();
            }
        }
    }

    private void CloseMenus()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    private void ControlsMenuActive()   //Enables controls menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    protected override void ExitGame()
    {
        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();

        SceneManager.LoadScene("TitleScreen");
    }

    public void PauseButton()
    {
        GameStateManager.Instance.SetPause();
    }

    public void Resume()
    {
        CloseMenus();
        GameStateManager.Instance.SetPause();
    }

    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();   //Should resume, as game should already be paused to access this button

        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToControls()
    {
        ControlsMenuActive();
    }
}
