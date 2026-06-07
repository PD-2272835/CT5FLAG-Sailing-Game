using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum InitialState    //For setting initial state of GameStateManager
{
    Menu,
    Gameplay,
    GameOver,
    Tutorial
}

//The Game State Manager should be used to orchestrate events such as pausing, game start, game end and switching between Main Menu and back
//This is a singleton state machine that will persist througout play and between scenes, with each state allowing 
public class GameStateManager : MonoBehaviour
{
    [SerializeField] private InitialState _setInitialState; //Menu by default

    public static GameStateManager Instance { get; private set; } //this state manager is a singleton (that requires access to monobehaviour elements)
    public static event Action<bool> OnPauseGame;

    private AbstractGameState _currentState;
    //state instances go here
    public MenuState Menu = new MenuState();
    public GameplayState Gameplay = new GameplayState();
    public GameOverState GameOver = new GameOverState();
    public TutorialState Tutorial = new TutorialState();
        
    public float InitialPlayerForwardSpeed = 10f;
    public float PlayerForwardSpeed = 0; //this gets overriden, the initial value should be set in gameplaystate

    public ObstacleSettings[] allObstacles; //use this to hold all obstacle settings for spawn weighting and pool creation
    
    public GameObject player; //terrible coupled way to get a reference to the player

    //Ensure Singleton
    void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);

        switch (_setInitialState)   
        {
            case InitialState.Menu:
                ChangeState(Menu);
                break;
            case InitialState.Gameplay:
                ChangeState(Gameplay);
                break;
            case InitialState.GameOver:
                ChangeState(GameOver);
                break;
            case InitialState.Tutorial:
                ChangeState(Tutorial);
                break;
        }
    }

    //allow the current state access to the update loop
    void Update()
    {
        _currentState?.Update(this);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += GetPlayer;
    }

    void GetPlayer(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainGameScene" || scene.name == "TutorialScene") this.player = GameObject.FindAnyObjectByType<PlayerStats>().gameObject;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GetPlayer;
    }


    //Coordinate game pausing
    public void SetPause(bool shouldPause)
    {
    
        if (shouldPause)
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
        }

        Debug.Log($"Called SetPause in GameStateManager with {shouldPause}");
        OnPauseGame?.Invoke(shouldPause);
    }

    public void OnApplicationPause(bool pause)
    {
        Time.timeScale = 0f;
        OnPauseGame?.Invoke(true);
    }

    public void OnApplicationFocus(bool focus)
    {
        //maybe some kind of subway surfers-style countdown?
        //or indeed wait until the pause screen goes away
        //this will be fine for now though
        
        Time.timeScale = 1f;
        OnPauseGame?.Invoke(false);
    }


    //Change State
    public void ChangeState(AbstractGameState newState)
    {

        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    //Return _currentState
    public AbstractGameState GetState()
    {
        return _currentState;
    }

    //Add to score
    public void AddScore(int score)
    {
        Gameplay.CurrentScore += score;
    }

    //Get score
    public int GetScore()
    {
        return Gameplay.CurrentScore;
    }

    //ResetScore
    public void ResetScore()
    {
        Gameplay.CurrentScore = 0;
    }

    public void StartObstacleRaise(ObstacleSettings settings, GameObject obj, float duration, float startHeight, float endHeight)
    {
        StartCoroutine(RaiseToHorizon(obj, duration, startHeight, endHeight));
    }

    //working out how to work with enumerators for interpolation between values
    //https://discussions.unity.com/t/ienumerator-with-transform-rotate-is-slighty-off/907397/7
    public IEnumerator RaiseToHorizon(GameObject obj, float duration, float startHeight, float endHeight)
    {

        float factor = 1f / duration;
        for (float time = 0f; time <= duration; time += Time.deltaTime * factor)
        {
            float t = time / duration;
            float progress = 1 - Mathf.Pow(1 - time, 3); //ease out cubic

            if (obj != null) obj.transform.position = new Vector3(obj.transform.position.x, Mathf.Lerp(startHeight, endHeight, progress), obj.transform.position.z);

            yield return null;
        }
        if (obj != null) obj.transform.position = new Vector3(obj.transform.position.x, endHeight, obj.transform.position.z);
    }
}
