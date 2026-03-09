using UnityEngine;

public class ObstacleSettings : FlyweightSettings
{
    public Cargo[] DamagesCargo;

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
