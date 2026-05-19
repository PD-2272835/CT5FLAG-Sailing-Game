using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int _cargoScore = 50;
    [SerializeField] private Transform _cargoLocation;

    ///private GameObject _cargoInstance = null;

    public int Health;
    public Cargo HeldCargo = null;

    public void TakeDamage()
    {

    }

    public void CargoNew(Cargo cargo, int amount)   //Called when recieving cargo from island
    {
        if (HeldCargo != null)  //Cargo will be ignored if the player is already carrying cargo
        {
            Debug.Log($"Cargo declined, player is carrying {HeldCargo}, {HeldCargo.CargoCount}");
        }
        else
        {
            HeldCargo = cargo;
            HeldCargo.CargoCount = amount;
            Debug.Log($"Player now has {HeldCargo}, {HeldCargo.CargoCount}");

            ///_cargoInstance = Instantiate(HeldCargo.Prefab, _cargoLocation);
        }
    }

    public void CargoDeliver()
    {
        if (HeldCargo != null) //Add _cargoScore multiplied by amount of cargo remaining to the current score
        {
            GameStateManager.Instance.AddScore(_cargoScore * HeldCargo.CargoCount);
            
            Destroy(HeldCargo);
            HeldCargo = null;

            ///Destroy(_cargoInstance.gameObject);
            ///_cargoInstance = null;
        }
        else
        {
            Debug.Log("Could not deliver cargo, player has none");
        }
    }

    public void CargoDamage()   // Called by OnTriggerEnter when colliding with valid obstacle
    {
        HeldCargo.TakeDamage();

        if (HeldCargo.CargoCount == 0)
        {
            Debug.Log("Cargo has been destroyed");
            Destroy(HeldCargo);
            HeldCargo = null;
        }
    }

    public void CargoRestore()  ///implement into OnTriggerEnter to call function when colliding with cargo obstacle
    {
        if (HeldCargo != null)
        {
            HeldCargo.HealDamage();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //each obstacle can define what cargo it can damage (more extensible)
        Cargo[] damagableCargos = other.GetComponent<ICargoDamager>()?.GetDamagableCargo();

        if (damagableCargos?.Length > 0)
        {
            for (int i = 0; i < damagableCargos.Length; ++i)
            {
                if (HeldCargo.GetType() == damagableCargos[i].GetType())
                {
                    CargoDamage();
                }
            }
        }
       
    }


}
