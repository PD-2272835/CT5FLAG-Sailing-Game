using UnityEngine;


//The Game State Manager should be used to handle events such as pausing, game start, game end and switching between Main Menu and back
//This is a singleton state machine that will persist througout play and between scenes, with each state allowing 
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; } //this state manager is a singleton

    private AbstractGameState _currentState;
    //state instances go here


    //state instances end here ^


    //Ensure Singleton
    void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }
        DontDestroyOnLoad(Instance);
    }



    //Change State
    public void ChangeState(AbstractGameState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

}
