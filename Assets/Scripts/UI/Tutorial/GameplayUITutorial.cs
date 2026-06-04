using UnityEngine;

public class GameplayUITutorial : MonoBehaviour
{
    //This script is an extension of GameplayUI to be used in TutorialScene
    //Only use this script when attaching to the same GameObject as GameplayUI in TutorialScene

    public static GameplayUITutorial Instance { get; private set; }

    private GameplayUI _gameplayUI;

    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }

        _gameplayUI = gameObject.GetComponent<GameplayUI>();
    }

    public void TutorialEnd()
    {
        Time.timeScale = 0.0f;

        _gameplayUI._gameOverMenu.SetActive(true);
        _gameplayUI._gameOverBackground.SetActive(true);
    }
}
