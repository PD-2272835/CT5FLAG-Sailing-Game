using UnityEngine;

public class TutorialState : AbstractGameState
{
    public override void EnterState(GameStateManager context)
    {
        Debug.Log("Entered TutorialState");
    }

    public override void ExitState(GameStateManager context)
    {
        Debug.Log("Exited TutorialState");
    }

    public override void Update(GameStateManager context)
    {

    }
}
