using UnityEngine;

public abstract class AbstractCargo : MonoBehaviour
{
    private int cargoCount;
    public int CargoCount
    {
        get { return cargoCount; }
        set
        {
            cargoCount = value;
            if (cargoCount < 0)
            {
                cargoCount = 0;
            }
        }
    }
    public void TakeDamage() ///will probably implement interface to handle damage method as player will also need it
    {
        CargoCount--;
    }
}
