using UnityEngine;

[CreateAssetMenu( fileName = "NewItem", menuName = "Cargo")]
public class Cargo : ScriptableObject, IDamageable
{
    private int cargoCount;
    public GameObject Prefab;
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
    public void TakeDamage()
    {
        CargoCount--;
    }
    public void HealDamage()
    {
        CargoCount++;
    }
}
