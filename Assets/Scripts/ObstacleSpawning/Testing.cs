using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour 
{
    public FlyweightSettings flyweight;


    private void Update()
    {
        if (Input.anyKey)
        {
            Flyweight flyweightInstace = FlyweightFactory.Spawn(flyweight);
            flyweightInstace.transform.position = transform.position;
        }
    }
}
