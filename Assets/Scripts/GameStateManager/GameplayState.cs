using System.Collections.Generic;
using UnityEngine;

public struct AvoidPoint
{
    public Transform t; //position
    public float r; //radius
}

public class GameplayState : AbstractGameState
{

    public int CurrentScore;

    private List<AvoidPoint> _AvoidPoints;
    private float ObstacleRandomXOffet => GetRandomXPosAvoid(); //like lanes, will be relative to player - should be configureable


    private float _CurrentObstacleInterval; //time between obstacles
    private float _CurrentIslandSpawnInterval; //interval between island spawns
    private float _LastIslandInterval;
    private float _LastObstacleInterval;

    private float _ElapsedTime;
    private float _TotalObstacleSpawnWeight;


    //Designers can tweak these to mess with how obstacles are spawned! >:3c
    private readonly float _InitialPlayerSpeed = 15f;           //Starting forward speed of the player
    private readonly float _MaxSpeed = 40f;                     //Maximum player speed
    private readonly float _DifficultyRampDuration = 20f;       //seconds to get to max difficulty (speed and obstacle intervals)
    private readonly float _InitialObstacleInterval = 1.5f;     //starting time between obstacles
    private readonly float _MaxSpawnInterval = 2f;              //maximum time between obstacles (seconds)
    private readonly float _HorizonDistance = 300f;             //how far in front of the player obstacles should spawn
    private readonly float _ObstacleXSpawningRange = 50f;       //how far to the left and right of the player an obstacle should be allowed to spawn
    private readonly float _InitialIslandSpawnInterval = 1f;    //starting minimum interval between islands
    private readonly float _IslandSpawnIntervalAmplifier = 2f;  //How much the island interval should increase by on each successful island spawn
    

    //this should reset/initialze the game state
    public override void EnterState(GameStateManager context)
    {
        //intial internal state should be set in here as modifiable values are not guarenteed to be correct
        CurrentScore = 0;
        _AvoidPoints = new List<AvoidPoint>();
        _LastIslandInterval = 1f;
        _LastObstacleInterval = 1f;
        _CurrentIslandSpawnInterval = _InitialIslandSpawnInterval;
        _CurrentObstacleInterval = _InitialObstacleInterval;
        _ElapsedTime = 0f;
        _TotalObstacleSpawnWeight = 0f;

        context.PlayerForwardSpeed = _InitialPlayerSpeed; //reset players speed on gameplay start
        foreach (var data in context.allObstacles)
        {
            _TotalObstacleSpawnWeight += data.SpawnWeight;
            //Debug.Log(_TotalObstacleSpawnWeight);
        }

        FlyweightFactory.ResetPools();
        InitializeObstaclePools(context);
    }

    public override void ExitState(GameStateManager context)    //GameStateManager should retrieve CurrentScore before exiting ExitState
    {
        
    }

    public override void Update(GameStateManager context)
    {
        _ElapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(_ElapsedTime / _DifficultyRampDuration);
        _CurrentObstacleInterval = Mathf.Lerp(_InitialObstacleInterval, _MaxSpawnInterval, t);
        context.PlayerForwardSpeed = Mathf.Lerp(_InitialPlayerSpeed, _MaxSpeed, t);

        //flow for this is as follows
        //pick an obstacle to spawn
        //-check elapsed interval between island checkpoints and determine wether an island can spawn
        //-allow picking island only if flag active
        //-otherwise only use weather/misc obstacles
        //get pool for that obstacle
        //find an appropriate location to spawn the selected obstacle
        //spawn it
        //Debug.Log(Time.deltaTime + " " + (_ElapsedTime - _LastObstacleInterval) + " " + _CurrentObstacleInterval);
        if(_ElapsedTime - _LastObstacleInterval > _CurrentObstacleInterval)
        {
            //Debug.Log("tried spawning something");
            ObstacleSettings obstacle = PickWeightedObstacle();
            GameObject obstacleObj = null;
            if (obstacle.Kind != ObstacleKind.Island)
            {
                obstacleObj = SpawnObstacle(obstacle);
            }
            else if (CanSpawnIsland())
            {
                obstacleObj = SpawnObstacle(obstacle);
            }

            if (obstacleObj != null)
            {
                context.StartObstacleRaise(obstacle, obstacleObj, 2f, obstacleObj.transform.position.y, 1f);
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


    private GameObject SpawnObstacle(ObstacleSettings settings)
    {
        Flyweight obstacle = FlyweightFactory.Spawn(settings);
        //obstacle.transform.position = Vector3.zero; //set new obstacle position
        if (settings.avoidPoints != null)
        {
            _AvoidPoints = new List<AvoidPoint>(settings.avoidPoints); //add the obstacle's avoid points to the _AvoidPoints array
        } else
        {
            _AvoidPoints.Clear(); //if there are no avoid points to add, clear the _AvoidPoints array
        }

        if (settings.Kind == ObstacleKind.Island)
        {
            obstacle.transform.position = GameStateManager.Instance.player.transform.position + new Vector3(0, -20, _HorizonDistance); //spawn all islands directly ahead of the player
            _LastIslandInterval = _ElapsedTime;
        }
        else
        {
            obstacle.transform.position = GameStateManager.Instance.player.transform.position + new Vector3(ObstacleRandomXOffet, -20, _HorizonDistance);
            _LastObstacleInterval = _ElapsedTime;
        }

        return obstacle.gameObject;
    }


    private bool CanSpawnIsland()
    {
        //TODO: island placement test - based on distance from last island?
        //float islandSpawnInterval = _CurrentSpawnInterval * 10;


        if (_ElapsedTime - _LastIslandInterval > _CurrentIslandSpawnInterval)
        {
            _CurrentIslandSpawnInterval = _CurrentObstacleInterval * _IslandSpawnIntervalAmplifier; //TODO: maybe move this somewhere else (like only if successful delivery)
            return true;
        }
        return false;
    }


    private float GetRandomXPosAvoid()
    {
        float desiredPosition = Random.Range(-(_ObstacleXSpawningRange - 1), _ObstacleXSpawningRange);

        //check every avoid point
        foreach (AvoidPoint ap in _AvoidPoints)
        {
            //check that the desired x offset is not within an avoid point's bounds, if it is, get another xpos
            if (desiredPosition < ap.t.transform.position.x + ap.r
                && desiredPosition > ap.t.transform.position.x - ap.r)
            {
                Debug.Log("chosen xpos was within spawn point");
                desiredPosition = GetRandomXPosAvoid();
                break;
            }
        }

        return desiredPosition;
    }

    private void InitializeObstaclePools(GameStateManager context)
    {
        foreach (ObstacleSettings settings in context.allObstacles)
        {
            Flyweight obstacle = FlyweightFactory.Spawn(settings);
            obstacle.transform.position = new Vector3(0, -40, 0);
            FlyweightFactory.ReturnToPool(obstacle);
        }
    }
}
