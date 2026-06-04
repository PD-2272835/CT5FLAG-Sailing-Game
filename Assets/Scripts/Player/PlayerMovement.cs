using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPausable
{
    [SerializeField] private float _speedHorizontal = 5f;

    private bool _isPaused;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    public bool DisableMovement = false;

    void OnEnable()
    {
        GameStateManager.OnPauseGame += OnPause;
    }
    void OnDisable()
    {
        GameStateManager.OnPauseGame -= OnPause;
    }

    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }
    public void OnPauseGame()
    {
        if (GameStateManager.Instance.GetState() != GameStateManager.Instance.GameOver)
        {
            GameStateManager.Instance.SetPause();
        }
    }


    public void OnPause(bool gamePauseState)
    {
        _isPaused = gamePauseState;
    }



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _isPaused = false;
    }
    void Start()
    {

    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!_isPaused && !DisableMovement)
        {
            _rb.linearVelocity = (new Vector2(_moveInput.x * _speedHorizontal, 0f));    ///needs to be smoothed
        }
    }
}
