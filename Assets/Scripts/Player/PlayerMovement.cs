using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedHorizontal = 5f;
    //[SerializeField] private float _speedForward = 5f;
    public bool IsFrozen;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        IsFrozen = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsFrozen)
        {
            rb.linearVelocity = (new Vector2(Input.GetAxis("Horizontal") * _speedHorizontal, 0f));
        }
    }
}
