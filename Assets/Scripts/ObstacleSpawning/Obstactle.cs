using UnityEngine;


//Obstacle is the generic movement behaviour of an obstacle - moving towards the player
public class Obstacle : MonoBehaviour, ICargoDamager
{
    public ObstacleSettings settings; // intrinsic state of obstacle flyweight
    public float speed = 1.0f; //this should be the players forwards moving speed and should be managed by an external class/game manager
    public float despawnBound; //TODO: this should also be pulled from a game manager of some kind

    public Cargo[] GetDamagableCargo() => settings.DamagesCargo;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime));

        if (transform.position.z < despawnBound)
        {
            ObstacleSpawner.ReturnToPool(this);
        }
    }
}
