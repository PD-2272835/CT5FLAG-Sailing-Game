using UnityEngine;


//Obstacle is the generic movement behaviour of an obstacle - moving towards the player
public class Obstacle : MonoBehaviour
{
    public ObstacleSettings settings;
    public float speed = 1.0f; //this should be the players forwards moving speed and should be managed by an external class/game manager
    public float despawnBound; //TODO: this should also be pulled from a game manager of some kind

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (speed * Time.fixedDeltaTime));

        if (transform.position.z < despawnBound)
        {
            ObstacleSpawner.ReturnToPool(this);
        }
    }
}
