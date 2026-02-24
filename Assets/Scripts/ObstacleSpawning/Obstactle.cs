using UnityEngine;


//Obstacle is the generic movement behaviour of an obstacle - moving towards the player
public class Obstacle : MonoBehaviour, ICargoDamager, IPausable
{
    public ObstacleSettings settings; // intrinsic state of obstacle flyweight
    public float Speed = 1.0f; //this should be the players forwards moving speed and should be managed by an external class/game manager
    public float DespawnBound; //TODO: this should also be pulled from a game manager of some kind
    private bool _isPaused = false;

    public Cargo[] GetDamagableCargo() => settings.DamagesCargo;

    public void OnPause(bool gamePauseState)
    {
        //I don't like this way of pausing, because FixedUpdate is still called, would ideally disable this behaviour script, but how would I re-enable it?
        //should be fine/working for now
        _isPaused = gamePauseState;
    }

    void FixedUpdate()
    {
        if(!_isPaused)
        {
            transform.Translate(Vector3.forward * (Speed * Time.fixedDeltaTime));

            if (transform.position.z < DespawnBound)
            {
                ObstacleSpawner.ReturnToPool(this);
            }
        }
        
    }
}
