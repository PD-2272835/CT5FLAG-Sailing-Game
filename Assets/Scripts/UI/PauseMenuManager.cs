using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuBase
{
    [SerializeField] private GameObject _pauseBackground;

    private void Awake()
    {
        fog = GameObject.FindGameObjectWithTag("TransitionFog").GetComponent<Image>();
        StartCoroutine(Transitions.FadeIn(fog, 1f));
        CloseMenus();
    }

    private void OnEnable()
    {
        AudioManager.OnMute += OnMute;
        GameStateManager.OnPauseGame += OnPause;
    }
    private void OnDisable()
    {
        AudioManager.OnMute -= OnMute;
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
        base.DisableAllMenusFunc(); ///Does pause menu also need the menu transitions?
        yield return StartCoroutine(base.MainMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator SettingsMenuActive()
    {
        base.DisableAllMenusFunc();
        StartCoroutine(base.SettingsMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator AudioMenuActive()
    {
        base.DisableAllMenusFunc();
        StartCoroutine(base.AudioMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator LanguageMenuActive()
    {
        base.DisableAllMenusFunc();
        StartCoroutine(base.LanguageMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator ControlsMenuActive()
    {
        base.DisableAllMenusFunc();
        StartCoroutine(base.ControlsMenuActive());
        _pauseBackground.SetActive(true);
        yield return null;
    }

    protected override IEnumerator ExitGame()
    {
        PlayUISound();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Menu);
        yield return StartCoroutine(Transitions.FadeOut(fog, 2f));
        SceneManager.LoadScene("TitleScreen");
        yield return null;
    }

    public void PauseButton()
    {
        PlayUISound();

        GameStateManager.Instance.SetPause();
    }

    public void Resume()
    {
        PlayUISound();

        CloseMenus();
        GameStateManager.Instance.SetPause();
    }


    public void Restart()
    {
       StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        PlayUISound();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);

        yield return StartCoroutine(Transitions.FadeOut(fog, 1f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
}
