using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IPausable
{
    [SerializeField] private float _speedHorizontal = 5f;

    private bool _isPaused;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    public bool CanPause = true;
    public bool DisableMovement = false;

    void OnEnable()
    {
        GameStateManager.OnPauseGame += OnPause;
        EnableInput(true);
    }
    void OnDisable()
    {
        GameStateManager.OnPauseGame -= OnPause;
        EnableInput(false);
    }

    private void EnableInput(bool enabled)  //Disables pause input if in TutorialScene, as pausing is handled in TutorialCameraController
    {
        PlayerInput playerInputs = GetComponent<PlayerInput>();
        InputActionMap playerMap = playerInputs.actions.FindActionMap("Player");
        InputActionMap tutorialMap = playerInputs.actions.FindActionMap("TutorialPlayer");

        if (enabled)
        {
            playerMap.Enable();
            tutorialMap.Disable();
        }
        else
        {
            playerMap.Disable();
            tutorialMap.Enable();
        }
        foreach (var map in GetComponent<PlayerInput>().actions.actionMaps)
        {
            Debug.Log($"{map.name}, {map.enabled}");
        }
        Debug.Log($"Called EnableInput with {enabled}");
    }

    private void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }
    public void OnPauseGame()
    {
        if (CanPause)
        {
            Debug.Log("Called OnPauseGame in PlayerMovement");
            if (GameStateManager.Instance.GetState() != GameStateManager.Instance.GameOver)
            {
                GameStateManager.Instance.SetPause(!_isPaused);
            }
        }
    }

    public void OnPause(bool gamePauseState)
    {
        Debug.Log($"Called OnPause in PlayerMovement with {gamePauseState}");
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
