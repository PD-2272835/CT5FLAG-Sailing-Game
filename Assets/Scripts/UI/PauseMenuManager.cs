using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
        if (GameStateManager.Instance.GetState() != GameStateManager.Instance.GameOver)
        {
            if (gamePauseState == true)
            {
                StartCoroutine(MainMenuActive());
            }
            else
            {
                CloseMenus();
            }
        }
    }

    private void CloseMenus()
    {
        base.DisableAllMenusFunc();
        _pauseBackground.SetActive(false);
    }

    protected override IEnumerator MainMenuActive()
    {
        yield return StartCoroutine(base.MainMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator SettingsMenuActive()
    {
        StartCoroutine(base.SettingsMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator AudioMenuActive()
    {
        StartCoroutine(base.AudioMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator LanguageMenuActive()
    {
        StartCoroutine(base.LanguageMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator ControlsMenuActive()
    {
        StartCoroutine(base.ControlsMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator ExitGame()
    {
        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Menu);

        SceneManager.LoadScene("TitleScreen");
        yield return null;
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

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
