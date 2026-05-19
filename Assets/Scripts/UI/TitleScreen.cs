using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MenuBase
{
    public GameObject CreditsMenu;
    public GameObject ControlsMenu;

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
        ControlsMenu.SetActive(false);

        BackgroundMain.SetActive(true);
        BackgroundCredCtrls.SetActive(false);
    }

    protected override void SettingsMenuActive()
    {
        base.SettingsMenuActive();
        CreditsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    private void CreditsMenuActive()  //Enables credits menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        ControlsMenu.SetActive(false);

        BackgroundMain.SetActive(false);
        BackgroundCredCtrls.SetActive(true);
    }

    private void ControlsMenuActive()   //Enables controls menu
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        AudioMenu.SetActive(false);
        LanguageMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        ControlsMenu.SetActive(true);

        BackgroundMain.SetActive(false);
        BackgroundCredCtrls.SetActive(true);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void GoToCredits()
    {
        CreditsMenuActive();
    }

    public void GoToControls()
    {
        ControlsMenuActive();
    }
}
