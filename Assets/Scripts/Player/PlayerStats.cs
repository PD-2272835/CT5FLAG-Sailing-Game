using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private AbstractCargo _heldCargo;

    public void CargoNew(AbstractCargo cargo, int amount)   //Called when recieving cargo from island
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
        if (_heldCargo != null)
        {
            ///add to some kind of score manager depending on amount of cargo delivered??
            
            Destroy(_heldCargo);
            _heldCargo = null;
        }
        else
        {
            Debug.Log("Could not deliver cargo, player has none");
        }
    }

    public void CargoDamage()   //Called by OnTriggerEnter when colliding with valid obstacle
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
        if (other.CompareTag("Obstacle") == true)
        {
            switch (other.name) ///adjust conditionals to match obstacles when they are implemented
            {
                case "Rock":
                    CargoDamage();
                    break;

                case "Rain":
                    if (_heldCargo is FruitCargo || _heldCargo is BookCargo)
                    {
                        CargoDamage();
                    }
                    break;

                case "Wave":
                    if (_heldCargo is FruitCargo || _heldCargo is AnimalCargo || _heldCargo is GlassCargo)
                    {
                        CargoDamage();
                    }
                    break;

                case "Cold":
                    if (_heldCargo is AnimalCargo)
                    {
                        CargoDamage();
                    }
                    break;

                case "Hot":
                    if (_heldCargo is GlassCargo)
                    {
                        CargoDamage();
                    }
                    break;
            }
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
