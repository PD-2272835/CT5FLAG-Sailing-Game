using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MenuBase
{
    public GameObject CreditsMenu;

    public GameObject BackgroundMain;
    public GameObject BackgroundCredCtrls;

    private void Awake()
    {
        MainMenuActive();
    }

    protected override void MainMenuActive()
    {
        base.MainMenuActive();
        CreditsMenu.SetActive(false);

        BackgroundMain.SetActive(true);
        BackgroundCredCtrls.SetActive(false);
    }

    protected override void SettingsMenuActive()
    {
        base.SettingsMenuActive();
        CreditsMenu.SetActive(false);

        BackgroundMain.SetActive(true);
        BackgroundCredCtrls.SetActive(false);
    }

    protected override void ControlsMenuActive()
    {
        base.ControlsMenuActive();
        CreditsMenu.SetActive(false);

        BackgroundMain.SetActive(false);
        BackgroundCredCtrls.SetActive(true);
    }

    private void CreditsMenuActive()  //Enables credits menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(true);

        BackgroundMain.SetActive(false);
        BackgroundCredCtrls.SetActive(true);
    }

    public void StartButton()
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);
        SceneManager.LoadScene("MainGameScene");
    }

    public void GoToCredits()
    {
        CreditsMenuActive();
    }
}
