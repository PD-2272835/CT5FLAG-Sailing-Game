using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

//Flyweight Factory Singleton that leverages object pooling to reduce Instantiate() and Destroy() overhead
//the spawn position of a flyweight should be defined by whatever class is creating a flyweight using transform.position
public static class FlyweightFactory // : MonoBehaviour
{
/*
    //ensure this is a singleton and is not destroyed upon changing scene
    public static FlyweightSpawner Instance;
    
    public void Awake()
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
*/

    //whether or not we want to catch doubly releasing a pooled object
    private static bool _collectionCheck = true;

    //Collection of object pools for Obstacles
    readonly static Dictionary<FlyweightSettings, IObjectPool<Flyweight>> ObstaclePools = new();



    //Spawn/Release an obstacle from it's respective pool
    public static Flyweight Spawn(FlyweightSettings settings) => GetPoolFor(settings)?.Get();
    public static void ReturnToPool(Flyweight flyweight) => GetPoolFor(flyweight.Settings)?.Release(flyweight);



    //Get the object pool for a provided obstacle, if no pool exists, create one
    public static IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
    {
        IObjectPool<Flyweight> pool = null;

        if (ObstaclePools.TryGetValue(settings, out pool)) return pool;

        pool = new ObjectPool<Flyweight>(
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
