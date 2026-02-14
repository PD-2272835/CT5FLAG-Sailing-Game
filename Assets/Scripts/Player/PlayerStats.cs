using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private AbstractCargo HeldCargo;

    public void CargoNew(AbstractCargo cargo, int amount)   //Called when recieving cargo from island
    {
        if (HeldCargo != null)  //Cargo will be ignored if the player is already carrying cargo
        {
            Debug.Log($"Cargo declined, player is carrying {HeldCargo}, {HeldCargo.GetCargoAmount()}");
        }
        else
        {
            HeldCargo = cargo;
            HeldCargo.SetCargoCount(amount);
            Debug.Log($"Player now has {HeldCargo}, {HeldCargo.GetCargoAmount()}");
        }
    }

    public void CargoDeliver()
    {
        if (HeldCargo != null)
        {
            ///add to some kind of score manager depending on amount of cargo delivered??
            
            Destroy(HeldCargo);
            HeldCargo = null;
        }
        else
        {
            Debug.Log("Could not deliver cargo, player has none");
        }
    }

    public void CargoDamage()   //Called by OnTriggerEnter when colliding with valid obstacle
    {
        HeldCargo.TakeDamage();

        if (HeldCargo.GetCargoAmount() == 0)
        {
            Debug.Log("Cargo has been destroyed");
            Destroy(HeldCargo);
            HeldCargo = null;
        }
    }


    ///not sure if OnTriggerEnter to damage cargo should be called here or in the obstacle?
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") == true)
        {
            ///conditionals to check if correct kind of obstacle to damage cargo
            //CargoDamage();
        }
    }

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
