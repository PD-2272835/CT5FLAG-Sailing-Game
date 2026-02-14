using UnityEngine;

public abstract class AbstractCargo : MonoBehaviour
{
    private int cargoCount;

    public void SetCargoCount(int amount)
    {
        cargoCount = amount;
    }
    public int GetCargoAmount()
    {
        return cargoCount;
    }
    public void TakeDamage() ///will probably implement interface to handle damage method as player will also need it
    {
        cargoCount--;

        if (cargoCount < 0)
        {
            cargoCount = 0;
        }
    }
}
