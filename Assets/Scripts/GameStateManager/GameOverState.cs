using UnityEngine;

public class GameOverState : AbstractGameState
{
    public override void EnterState(GameStateManager context)
    {
        Debug.Log("Entered GameOverState");
    }
    
    public override void ExitState(GameStateManager context)
    {
        Debug.Log("Exiting GameOverState");
    }

    public override void Update(GameStateManager context)
    {
        
    }
}
