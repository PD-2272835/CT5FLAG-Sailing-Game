using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMovement : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Transform _startingPos;
    private bool _active = false;

    private void Awake()
    {
        _rb = _player.GetComponent<Rigidbody>();
        
    }
    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }

    public void SetActive(bool b)
    {
        _active = b;

        _rb.linearVelocity = new Vector2(0, 0);
        if (b == false) _startingPos = _player.transform;
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            _rb.linearVelocity = new Vector2(_moveInput.x * 5, 0);
        }
    }
}
