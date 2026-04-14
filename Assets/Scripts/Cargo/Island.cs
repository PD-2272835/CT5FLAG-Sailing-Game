using Unity.VisualScripting;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private Cargo[] availableCargo;
    private bool islandVisited;
    private Cargo chosenCargo;
    /// serialized int for cargo amount?
    
    void Awake()
    {
        if (availableCargo == null)
        {
            islandVisited = true;
            Debug.LogWarning("No cargo assigned to island");
        }
        else
        {
            islandVisited = false;
        }
    }

    private void Start()
    {
        int random = Random.Range(0, availableCargo.Length);

        chosenCargo = availableCargo[random];
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            if (islandVisited == false || playerStats._heldCargo == null)
            {
                playerStats.CargoNew(chosenCargo, 5);
                islandVisited = true;
            }
            else if (islandVisited == false || playerStats._heldCargo != null)
            {
                playerStats.CargoDeliver();
                islandVisited = true;
            }
        }
    }
}
