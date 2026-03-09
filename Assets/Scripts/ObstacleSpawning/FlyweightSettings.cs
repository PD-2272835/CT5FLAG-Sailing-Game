using UnityEngine;

//Obstacle Settings is used to configure and manage an object in the Object Pooling for obstacles of any type
[CreateAssetMenu(fileName = "New Flyweight", menuName = "Flyweights")]
public class FlyweightSettings : ScriptableObject
{
    //If an obstacle has custom behaviour (eg. weather, island mechanics)
    //it should be attached as another component to the desired prefab here:
    //(this will be refactored soon, as this is not intended behaviour)
    [SerializeField] protected GameObject _Prefab;

    //allow customization of the max pool size and starting capacity
    public int PoolStartCapacity { get; private set; } = 10;
    public int PoolMaxSize {get; private set;} = 20;

    public float DespawnBoundZ = -10;


    //executed when the pool needs a completely new object
    //(eg. if there are no objects of this type in a pool)
    public virtual Flyweight CreatePoolObject()
    {
        GameObject prefabInstance = Instantiate(_Prefab);

        prefabInstance.SetActive(false);
        prefabInstance.name = _Prefab.name;

        var flyweight = prefabInstance.AddComponent<Flyweight>();
        flyweight.Settings = this;

        return flyweight;
    }


    public virtual void OnGetPoolObject(Flyweight flyweight) => flyweight.gameObject.SetActive(true);
    public virtual void OnReleasePoolObject(Flyweight flyweight) => flyweight.gameObject.SetActive(false);
    public virtual void OnDestroyPoolObject(Flyweight flyweight) => Destroy(flyweight.gameObject);
}
