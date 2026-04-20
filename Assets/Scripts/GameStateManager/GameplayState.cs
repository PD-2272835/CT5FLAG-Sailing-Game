using UnityEngine;

public class GameplayState : AbstractGameState
{

    public int CurrentScore = 0;
    private float ObstacleRandomXOffet => Random.Range(1, 20);

    private float _DistanceCovered = 0;
    private readonly float _YBetweenObstacles = 30;

    public float InitialPlayerForwardSpeed = 10;


    public override void EnterState(GameStateManager context)
    {
        GameStateManager.Instance.PlayerForwardSpeed = InitialPlayerForwardSpeed; //reset players speed on gameplay start


    }

    public override void ExitState(GameStateManager context)    //GameStateManager should retrieve CurrentScore before exiting ExitState
    {
        
    }

    public override void Update(GameStateManager context)
    {
        //pick an obstacle to spawn
        //-check elapsed distance between island checkpoints and set flag
        //-allow picking island only if flag active
        //-otherwise only use weather/misc obstacles
        //get pool for that obstacle
        //spawn it
    }



}
