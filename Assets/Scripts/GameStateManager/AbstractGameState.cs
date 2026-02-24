public abstract class AbstractGameState
{
    public abstract void EnterState(GameStateManager context);
    public abstract void ExitState(GameStateManager context);
    public abstract void Update(GameStateManager context);
}
