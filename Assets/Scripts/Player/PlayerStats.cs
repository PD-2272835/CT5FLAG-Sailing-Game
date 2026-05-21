using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int _cargoScore = 50;
    [SerializeField] private Transform _cargoLocation;

    //private GameObject _cargoInstance = null;

    public int Health = 3;
    public Cargo HeldCargo = null;

    public void TakeDamage()
    {
        Debug.Log("player has been damaged");
        Health--;

        //UI should be notified by an event delegate from here that the UI can subscribe to
        GameplayUI.Instance.DamageTaken(Health);

        if (Health <= 0)
        {
            gameObject.transform.Find("new pirate ship 5 fbx").gameObject.SetActive(false);
            GameplayUI.Instance.GameOver();
        }
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

            GameplayUI.Instance.DisplayCargo(HeldCargo);
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

            GameplayUI.Instance.DisplayCargo(null);
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
            Destroy(HeldCargo);
            HeldCargo = null;

            GameplayUI.Instance.DisplayCargo(null);

            Debug.Log("Cargo has been destroyed");
        }
    }

    public void CargoRestore()  //implement into OnTriggerEnter to call function when colliding with cargo obstacle
    {
        if (HeldCargo != null)
        {
            HeldCargo.HealDamage();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        ICargoDamager obstacle = other.GetComponent<ICargoDamager>();

        //each obstacle can define what cargo it can damage (more extensible)
        Cargo[] damagableCargos = obstacle?.GetDamagableCargo();

        if (damagableCargos?.Length > 0)
        {
            for (int i = 0; i < damagableCargos.Length; ++i)
            {
                Debug.Log(damagableCargos[i].ToString());
                if (HeldCargo?.GetType() == damagableCargos[i].GetType())
                {
                    CargoDamage();
                }
            }
        } else
        {
            //logic for if player should be damaged - simple take damage for now
            TakeDamage();
        }

    }
}
