using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Cargo _heldCargo;

    public void CargoNew(Cargo cargo, int amount)   //Called when recieving cargo from island
    {
        if (_heldCargo != null)  //Cargo will be ignored if the player is already carrying cargo
        {
            Debug.Log($"Cargo declined, player is carrying {_heldCargo}, {_heldCargo.CargoCount}");
        }
        else
        {
            _heldCargo = cargo;
            _heldCargo.CargoCount = amount;
            Debug.Log($"Player now has {_heldCargo}, {_heldCargo.CargoCount}");
        }
    }

    public void CargoDeliver()
    {
        if (_heldCargo != null) //Add x points multiplied by amount of cargo remaining
        {
            GameStateManager.Instance.AddScore(50 * _heldCargo.CargoCount);
            
            Destroy(_heldCargo);
            _heldCargo = null;
        }
        else
        {
            Debug.Log("Could not deliver cargo, player has none");
        }
    }

    public void CargoDamage()   // Called by OnTriggerEnter when colliding with valid obstacle
    {
        _heldCargo.TakeDamage();

        if (_heldCargo.CargoCount == 0)
        {
            Debug.Log("Cargo has been destroyed");
            Destroy(_heldCargo);
            _heldCargo = null;
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
                if (_heldCargo.GetType() == damagableCargos[i].GetType())
                {
                    CargoDamage();
                }
            }
        }
       
    }


}
