using UnityEngine;

public class GameplayState : AbstractGameState
{
    public int CurrentScore = 0;

    public override void EnterState(GameStateManager context)
    {
        
    }

    public override void ExitState(GameStateManager context)    //GameStateManager should retrieve CurrentScore before exiting ExitState
    {

    }

    public override void Update(GameStateManager context)
    {

    }
}
