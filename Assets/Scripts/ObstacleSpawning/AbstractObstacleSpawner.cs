using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//Flyweight Factory Singleton that leverages object pooling to reduce Instantiate() and Destroy() overhead
//the spawn position of a flyweight should be defined by whatever class is creating a flyweight using transform.position
public class ObstacleSpawner : MonoBehaviour
{

    //ensure this is a singleton and is not destroyed upon changing scene
    public static ObstacleSpawner Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }


    //wether or not we want to catch doubly releasing a pooled object
    [SerializeField]private bool _collectionCheck = true;

    //Collection of object pools for Obstacles
    readonly Dictionary<ObstacleSettings, IObjectPool<Obstacle>> ObstaclePools = new();



    //Spawn/Release an obstacle from it's respective pool
    public static Obstacle Spawn(ObstacleSettings settings) => Instance.GetPoolFor(settings)?.Get();
    public static void ReturnToPool(Obstacle flyweight) => Instance.GetPoolFor(flyweight.settings)?.Release(flyweight);



    //Get the object pool for a provided obstacle, if no pool exists, create one
    public IObjectPool<Obstacle> GetPoolFor(ObstacleSettings settings)
    {
        IObjectPool<Obstacle> pool = null;

        if (ObstaclePools.TryGetValue(settings, out pool)) return pool;

        pool = new ObjectPool<Obstacle>(
            settings.CreatePoolObject,
            settings.OnGetPoolObject,
            settings.OnReleasePoolObject,
            settings.OnDestroyPoolObject,
            _collectionCheck,
            settings.PoolStartCapacity,
            settings.PoolMaxSize
        );

        ObstaclePools.Add(settings, pool);
        return pool;
    }
}
