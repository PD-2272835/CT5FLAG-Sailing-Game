using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManagerTutorial : MonoBehaviour
{
    //This script is an extension of PauseMenuManager to be used in TutorialScene
    //Only use this script when attaching to the same GameObject as PauseMenuManager in TutorialScene

    private PauseMenuManager _pauseMenuManager;

    private void Awake()
    {
        _pauseMenuManager = gameObject.GetComponent<PauseMenuManager>();
    }

    private IEnumerator StartMain()
    {
        _pauseMenuManager.PlayUISound();

        GameStateManager.Instance.ResetScore();
        GameStateManager.Instance.SetPause();
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.Gameplay);

        yield return StartCoroutine(Transitions.FadeOut(_pauseMenuManager.Fog, 2f));
        SceneManager.LoadScene("MainGameScene");
        yield return null;
    }

    public void StartButton()
    {
        StartCoroutine(StartMain());
    }
}
