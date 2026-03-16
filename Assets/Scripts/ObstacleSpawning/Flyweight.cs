using UnityEngine;


//Obstacle is the generic movement behaviour of an obstacle - moving towards the player
public class Flyweight : MonoBehaviour
{
    public FlyweightSettings Settings; // intrinsic state of flyweight

    public float Speed = 1.0f; //this should be the players forwards moving speed and should be managed by an external class/game manager


    //if using FixedUpdate in a derived class,
    //call base.FixedUpdate() to maintain movement towards the player (if you need this object to still move)
    public virtual void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (Speed * Time.fixedDeltaTime));

        if (transform.position.z < Settings.DespawnBoundZ)
        {
            FlyweightFactory.ReturnToPool(this);
        }
    }
}
