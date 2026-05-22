using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuBase
{
    [SerializeField] private GameObject _pauseBackground;

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
        /// if (GameStateManager.Instance.CurrentState != EndGame)
        if (gamePauseState == true)
        {
            MainMenuActive();
        }
        else
        {
            CloseMenus();
        }
    }

    private void CloseMenus()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        _pauseBackground.SetActive(false);
    }

    protected override void MainMenuActive()
    {
        base.MainMenuActive();
        _pauseBackground.SetActive(true);
    }

    protected override void SettingsMenuActive()
    {
        base.SettingsMenuActive();
        _pauseBackground.SetActive(true);
    }

    protected override void AudioMenuActive()
    {
        base.AudioMenuActive();
        _pauseBackground.SetActive(true);
    }

    protected override void LanguageMenuActive()
    {
        base.LanguageMenuActive();
        _pauseBackground.SetActive(true);
    }

    protected override void ControlsMenuActive()
    {
        base.ControlsMenuActive();
        _pauseBackground.SetActive(true);
    }

    protected override void ExitGame()
    {
        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Menu);

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
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);

        SceneManager.LoadScene(currentScene.name);
    }
}
