using System;
using UnityEngine;


//The Game State Manager should be used to orchestrate events such as pausing, game start, game end and switching between Main Menu and back
//This is a singleton state machine that will persist througout play and between scenes, with each state allowing 
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; } //this state manager is a singleton (that requires access to monobehaviour elements)
    public static event Action<bool> OnPauseGame;

    private AbstractGameState _currentState;
    //state instances go here
    public MenuState Menu = new MenuState();
    public GameplayState Gameplay = new GameplayState();

    public float PlayerForwardSpeed;

    //Ensure Singleton
    void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);

        ChangeState(Menu); //initial state
    }

    //allow the current state access to the update loop
    void Update()
    {
        _currentState?.Update(this);
    }


    //Coordinate game pausing
    public void SetPause(bool setPause)
    {
        OnPauseGame?.Invoke(setPause);

        if (setPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    //Change State
    public void ChangeState(AbstractGameState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    //Add to score
    public void AddScore(int score)
    {
        Gameplay.CurrentScore += score;
    }
}
