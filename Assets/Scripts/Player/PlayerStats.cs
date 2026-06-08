using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int _cargoScore = 50;
    [SerializeField] private Transform _cargoLocation;
    [SerializeField] private AudioClip _playerDamageSound;

    private GameObject _cargoInstance = null;
    private AudioSource _playerAudio;

    public int Health = 3;
    public Cargo HeldCargo = null;

    private void Awake()
    {
        _playerAudio = GetComponent<AudioSource>();
        Debug.Log($"Player has {Health} health on Awake");
    }
    public void TakeDamage()
    {
        Debug.Log("player has been damaged");
        Health--;
        Debug.Log($"Player now has {Health} health");

        ///UI should be notified by an event delegate from here that the UI can subscribe to
        GameplayUI.Instance?.DamageTaken(Health);
        _playerAudio.PlayOneShot(_playerDamageSound);

        if (Health <= 0)
        {
            gameObject.transform.Find("new pirate ship 5 fbx").gameObject.SetActive(false);
            if (GameplayUITutorial.Instance)    //Only call GameOver if the player does not die in the tutorials
            {
                GameplayUITutorial.Instance.TutorialEnd();
            }
            else
            {
                GameplayUI.Instance?.GameOver();
            }
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

            _playerAudio.PlayOneShot(HeldCargo.PickupSound);
            GameplayUI.Instance?.DisplayCargo(HeldCargo);

            _cargoInstance = Instantiate(HeldCargo.Prefab, _cargoLocation.position, _cargoLocation.rotation, _cargoLocation);
        }
    }

    public void CargoDeliver()
    {
        if (HeldCargo != null) //Add _cargoScore multiplied by amount of cargo remaining to the current score
        {
            GameStateManager.Instance.AddScore(_cargoScore * HeldCargo.CargoCount);

            HeldCargo = null;

            GameplayUI.Instance?.DisplayCargo(null);
            GameplayUI.Instance?.DisplayScore(GameStateManager.Instance.GetScore());

            Destroy(_cargoInstance.gameObject);
            _cargoInstance = null;
        }
        else
        {
            Debug.Log("Could not deliver cargo, player has none");
        }
    }

    public void CargoDamage()   // Called by OnTriggerEnter when colliding with valid obstacle
    {
        HeldCargo.TakeDamage();
        
        _playerAudio.PlayOneShot(HeldCargo.DamageSound);

        if (HeldCargo.CargoCount == 0)
        {
            HeldCargo = null;

            GameplayUI.Instance?.DisplayCargo(null);
            
            foreach(Transform child in _cargoLocation) //remove the cargo prefab instance from visibility on the player ship
            {
                Destroy(child.gameObject);
            }

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

        Debug.Log(obstacle?.ToString());

        //each obstacle can define what cargo it can damage (more extensible)
        Cargo[] damagableCargos = obstacle?.GetDamagableCargo();

        Debug.Log($"entered collider of {other.name} where cargos are {damagableCargos?.Length}");

        if (damagableCargos?.Length > 0)
        {
            for (int i = 0; i < damagableCargos.Length; ++i)
            {
                Debug.Log(damagableCargos[i].ToString());
                if (HeldCargo?.name == damagableCargos[i].name)
                {
                    Debug.Log($"Damaged Cargo {HeldCargo}, when against {damagableCargos[i]}");
                    CargoDamage();
                }
            }
        } 


        if (!other.CompareTag("DockCollider") && !other.CompareTag("Weather")) //if it's not a dock collider or weather, we should damage the player
        {
            Debug.Log(other.name + " was not weather/a dock collider");
            //logic for if player should be damaged - simple take damage for now
            TakeDamage();
        }

    }
}
