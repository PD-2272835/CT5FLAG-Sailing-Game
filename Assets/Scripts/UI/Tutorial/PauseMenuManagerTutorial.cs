using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManagerTutorial : PauseMenuManager
{
    private IEnumerator StartMain()
    {
        PlayUISound();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Menu);
        yield return StartCoroutine(Transitions.FadeOut(fog, 2f));
        SceneManager.LoadScene("MainGameScene");
        yield return null;
    }

    public void StartButton()
    {
        StartCoroutine(StartMain());
    }
}
