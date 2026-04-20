using UnityEngine;

public class GameplayState : AbstractGameState
{

    public int CurrentScore = 0;
    private float ObstacleRandomXOffet => Random.Range(1, 20); //like lanes, will be relative to player

    private float _DistanceCovered = 0f;
    private float _LastIslandLocation = 0f;

    private float _ElapsedTime = 0f;
    private float _TotalObstacleSpawnWeight = 0f;

    private float _InitialSpawnInterval = 100f;
    private float _CurrentSpawnInterval; //time between obstacles
    private float _DifficultyRampDuration = 300f; //seconds to get to max difficulty
    private float _MinSpawnInterval = 0.5f; //minimum time between obstacles (seconds)

    //this should reset/initialze the game state
    public override void EnterState(GameStateManager context)
    {
        context.PlayerForwardSpeed = 10f; //reset players speed on gameplay start
        _CurrentSpawnInterval = 0f;

        foreach(var data in context.allObstacles)
        {
            _TotalObstacleSpawnWeight += data.SpawnWeight;
        }
    }

    public override void ExitState(GameStateManager context)    //GameStateManager should retrieve CurrentScore before exiting ExitState
    {
        
    }

    public override void Update(GameStateManager context)
    {
        _ElapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(_ElapsedTime / _DifficultyRampDuration);
        _CurrentSpawnInterval = Mathf.Lerp(_InitialSpawnInterval, _MinSpawnInterval, t);


        //flow for this is as follows
        //pick an obstacle to spawn
        //-check elapsed distance:speed between island checkpoints and set flag
        //-allow picking island only if flag active
        //-otherwise only use weather/misc obstacles
        //get pool for that obstacle
        //find an appropriate location to spawn the selected obstacle
        //spawn it
        ObstacleSettings obstacle = PickWeightedObstacle();

        if (obstacle.Kind != ObstacleKind.Island)
        {
            SpawnObstacle(obstacle);
        }
        else
        {
            if (CanSpawnIsland())
            {
                SpawnObstacle(obstacle);
            }
        }
    }



    private ObstacleSettings PickWeightedObstacle()
    {
        float roll = Random.Range(0, _TotalObstacleSpawnWeight);
        float cumulative = 0;
        foreach(var data in GameStateManager.Instance.allObstacles)
        {
            if (roll <= cumulative)
            {
                return data;
            }
            cumulative += data.SpawnWeight;
        }

        //safe exit if no valid obstacle is found
        //this means that allObstacles should probably start with a safe object, like a rock
        return GameStateManager.Instance.allObstacles[0];
    }


    private void SpawnObstacle(ObstacleSettings settings)
    {
        Flyweight obstacle = FlyweightFactory.Spawn(settings);
        obstacle.transform.position = Vector3.zero; //set new obstacle position
        //TODO: determine position
    }

    private bool CanSpawnIsland()
    {
        //TODO: island placement test - based on distance from last island?
        return false;
    }
}
