using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MenuBase
{
    public GameObject CreditsMenu;

    public GameObject BackgroundMain;
    public GameObject BackgroundCredCtrls;

    private void Awake()
    {
        fog = GameObject.FindGameObjectWithTag("TransitionFog").GetComponent<Image>();
        Debug.Log(fog);
        transitionDuration = 0.5f;
        MainMenu.SetActive(true);
        StartCoroutine(Transitions.FadeIn(fog, 1f));
    }

    public override void DisableAllMenusFunc()
    {
        base.DisableAllMenusFunc();
    }

    public override IEnumerator DisableAllMenus()
    {
        yield return StartCoroutine (base.DisableAllMenus());
        CreditsMenu.SetActive(false);
        BackgroundMain.SetActive(false);
        BackgroundCredCtrls.SetActive(false);
        yield return null;
    }

    protected override IEnumerator MainMenuActive()
    {
        yield return StartCoroutine(DisableAllMenus());
        yield return StartCoroutine(base.MainMenuActive());
        BackgroundMain.SetActive(true);
        yield return StartCoroutine(Transitions.FadeIn(fog, transitionDuration));
    }

    protected override IEnumerator SettingsMenuActive()
    {
        yield return StartCoroutine(DisableAllMenus());
        BackgroundMain.SetActive(true);
        yield return StartCoroutine(base.SettingsMenuActive());
        yield return StartCoroutine(Transitions.FadeIn(fog, transitionDuration));
    }

    protected override IEnumerator ControlsMenuActive()
    {
        yield return StartCoroutine(DisableAllMenus());
        BackgroundCredCtrls.SetActive(true);
        yield return StartCoroutine(base.ControlsMenuActive());
        yield return StartCoroutine(Transitions.FadeIn(fog, transitionDuration));
    }

    private IEnumerator CreditsMenuActive()  //Enables credits menu
    {
        yield return StartCoroutine(DisableAllMenus());
        CreditsMenu.SetActive(true);
        BackgroundCredCtrls.SetActive(true);
        yield return StartCoroutine(Transitions.FadeIn(fog, transitionDuration));
    }

    public void GoToSettingsNoTransition()
    {
        PlayUISound();

        base.DisableAllMenusFunc();
        StartCoroutine(base.SettingsMenuActive());
    }


    public void StartButton()
    {
        PlayUISound();

        if (GameStateManager.Instance)
        {
            Debug.Log("Changed to GameplayState");
            GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);
        }

        StartCoroutine(LoadGame("MainGameScene"));
    }

    public void TutorialButton()
    {
        PlayUISound();

        if (GameStateManager.Instance)
        {
            Debug.Log("Changed to TutorialState");
            GameStateManager.Instance.ChangeState(GameStateManager.Instance.Tutorial);
        }

        StartCoroutine(LoadGame("TutorialScene"));
    }

    private IEnumerator LoadGame(string sceneName)
    {
        yield return StartCoroutine(Transitions.FadeOut(fog, 2.3f));
        SceneManager.LoadScene(sceneName);
    }

    public void GoToCredits()
    {
        PlayUISound();

        StartCoroutine(CreditsMenuActive());
    }

}
