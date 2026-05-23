using UnityEngine;

public enum ObstacleKind //flags to help with obstacle spawn determination
{
    Misc = 0, //rocks, other obstacles
    Island = 1, //islands
    Weather = 2 //weather
};

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Flyweights/Obstacle")]
public class ObstacleSettings : FlyweightSettings
{
    
    public Cargo[] DamagesCargo; //cargo which this obstacle should damage. If Length 0, this obstacle will damage the player
    public float SpawnWeight;
    public ObstacleKind Kind = 0;
    public AvoidPoint[] avoidPoints;

    public override Flyweight CreatePoolObject()
    {
        GameObject prefabInstance = Instantiate(_Prefab);

        prefabInstance.SetActive(false);
        prefabInstance.name = _Prefab.name;

        var flyweight = prefabInstance.AddComponent<Obstacle>();
        flyweight.Settings = this;

        return flyweight;
    }
}
