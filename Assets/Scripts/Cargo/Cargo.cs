using UnityEngine;
using System.Collections;

[CreateAssetMenu( fileName = "NewItem", menuName = "Cargo")]
public class Cargo : ScriptableObject, IDamageable
{
    private int cargoCount;
    public bool isInDamagableWeather;
    public GameObject Prefab;
    public AudioClip PickupSound;
    public AudioClip DamageSound;

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

    public IEnumerator StartCargoDamage(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (isInDamagableWeather)
        {
            TakeDamage();
            yield return StartCargoDamage(interval);
        }
        yield return null;
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
