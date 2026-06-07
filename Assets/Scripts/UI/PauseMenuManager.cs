using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuBase
{
    [SerializeField] private GameObject _pauseBackground;

    private void Awake()
    {
        Fog = GameObject.FindGameObjectWithTag("TransitionFog").GetComponent<Image>();
        StartCoroutine(Transitions.FadeIn(Fog, 1f));
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
        GameStateManager.Instance.SetPause(false);
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Menu);
        yield return StartCoroutine(Transitions.FadeOut(Fog, 2f));
        SceneManager.LoadScene("TitleScreen");
        yield return null;
    }

    public void PauseButton()
    {
        Debug.Log("Called PauseButton");
        PlayUISound();

        GameStateManager.Instance.SetPause((Time.timeScale > 0));
    }

    public void Resume()
    {
        Debug.Log("Called Resume");
        PlayUISound();

        CloseMenus();
        GameStateManager.Instance.SetPause(false);
    }


    public void Restart()
    {
       StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        PlayUISound();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause(false);
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);

        yield return StartCoroutine(Transitions.FadeOut(Fog, 1f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return null;
    }
}
