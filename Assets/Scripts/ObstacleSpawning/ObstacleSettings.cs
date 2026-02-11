using UnityEngine;

//Obstacle Settings is used to configure and manage an object in the Object Pooling for obstacles of any type
public class ObstacleSettings : ScriptableObject
{
    //If an obstacle has custom behaviour (eg. weather, island mechanics)
    //it should (CURRENTLY) be attached as another component to the desired prefab here:
    //(this will be refactored soon, as this is not intended behaviour)
    [SerializeField] private GameObject prefab;

    //allow customization of the max pool size and starting capacity
    public int PoolStartCapacity { get; private set; } = 10;
    public int PoolMaxSize {get; private set;} = 20;


    //executed when the pool needs a completely new object
    //(eg. if there are no objects of this type in a pool)
    public Obstacle CreatePoolObject()
    {
        GameObject prefabInstance = Instantiate(prefab);

        prefabInstance.SetActive(false);
        prefabInstance.name = prefab.name;

        var flyweight = prefabInstance.AddComponent<Obstacle>();
        flyweight.settings = this;

        return flyweight;
    }


    public void OnGetPoolObject(Obstacle flyweight) => flyweight.gameObject.SetActive(true);
    public void OnReleasePoolObject(Obstacle flyweight) => flyweight.gameObject.SetActive(false);
    public void OnDestroyPoolObject(Obstacle flyweight) => Destroy(flyweight.gameObject);
}
