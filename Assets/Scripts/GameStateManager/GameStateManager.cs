using System;
using UnityEngine;


//The Game State Manager should be used to orchestrate events such as pausing, game start, game end and switching between Main Menu and back
//This is a singleton state machine that will persist througout play and between scenes, with each state allowing 
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; } //this state manager is a singleton
    public static event Action<bool> OnPauseGame;

    private AbstractGameState _currentState;
    //state instances go here
    public MenuState Menu = new MenuState();
    public GameplayState Gameplay = new GameplayState();
    //public PauseState Pause = new PauseState(); //unsure if this is needed? may be too much to have a whole new game state for just pausing - might work if was in a concurrent state machine


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

    void Update()
    {
        _currentState?.Update(this);
    }


    //Coordinate game pausing
    public void SetPause(bool setPause)
    {
        OnPauseGame?.Invoke(setPause);
    }


    //Change State
    public void ChangeState(AbstractGameState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

}
