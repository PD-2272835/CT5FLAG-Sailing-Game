
//all pausables should subscribe to the GameStateManager's OnPauseGame event action
//
//GameStateManager.Instance += OnPause; in OnEnable() to subscribe
//GameStateManager.Instance -= OnPause; in OnDisable() to unsubscribe


public interface IPausable
{
    void OnPause(bool gamePauseState);
}
