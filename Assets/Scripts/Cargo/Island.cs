using Unity.VisualScripting;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private Cargo[] availableCargo;
    [SerializeField] private Transform _cargoLocation;

    private bool islandVisited;
    private Cargo chosenCargo;
    private GameObject _cargoInstance;
    
    void Awake()
    {
        Debug.Log("Island instantiated");
    }

    private void OnEnable()
    {
        if (availableCargo == null)
        {
            islandVisited = true;
            Debug.LogWarning("No cargo assigned to island");
        }
        else
        {
            islandVisited = false;

            int random = Random.Range(0, availableCargo.Length);

            chosenCargo = availableCargo[random];
            Debug.Log($"Island cargo is {chosenCargo.name}");

            ///_cargoInstance = Instantiate(chosenCargo.Prefab, _cargoLocation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            if (islandVisited == false && playerStats.HeldCargo == null)
            {
                playerStats.CargoNew(chosenCargo, 1);
                islandVisited = true;

                ///_cargoInstance = null;

                Debug.Log($"Island has given {chosenCargo.name} cargo");
            }
            else if (islandVisited == false && playerStats.HeldCargo != null)
            {
                playerStats.CargoDeliver();
                playerStats.CargoNew(chosenCargo, 1);
                islandVisited = true;

                Debug.Log($"Island has taken player cargo");
            }
        }
    }
}
