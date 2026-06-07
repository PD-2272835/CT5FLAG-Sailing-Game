using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCameraController : MonoBehaviour
{

    //IMPORTANT: THESE SHOULD LINE UP AND CORRESPOND WITH EACH OTHER
    [SerializeField] private List<Transform> _Positions;
    [SerializeField] private List<GameObject> _Tooltips;

    //camera movement parameters
    [SerializeField] private float _MovementSpeed = 100f;
    [SerializeField] private float _RotationSpeed = 10f;

    //track the current state of the tutorial
    [SerializeField] private bool _atTargetTransform = false;
    [SerializeField] private int _targetIndex;

    [SerializeField] private int _movementIndex; //Index of the tutorial stage that should allow movement

    private TutorialMovement _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<TutorialMovement>();

        GetComponent<PlayerInput>().actions.FindActionMap("Player").Disable();

        foreach (var map in GetComponent<PlayerInput>().actions.actionMaps)
        {
            Debug.Log($"{map.name}, {map.enabled}");
        }
    }

    private void OnPrevious(InputValue input)
    {
        Debug.Log("Called OnPrevious");
        if (_targetIndex == 0) return;
        _targetIndex--;
        _atTargetTransform = false;

        Debug.Log($"_targetIndex moved to {_targetIndex}");
        CheckMovement();
    }

    private void OnNext(InputValue input)
    {
        Debug.Log("Called OnNext");

        _targetIndex++;
        if (_targetIndex > _Positions.Count - 1)
        {
            GameplayUITutorial.Instance.TutorialEnd();
            _targetIndex--;
            return;
        }
        else
        {
            Debug.Log($"_targetIndex moved to {_targetIndex}");
            CheckMovement();
        }
        _atTargetTransform = false;
    }

    private void OnPauseGame()
    {
        Debug.Log("Called OnPauseGame in TutorialCameraController");

        GameStateManager.Instance.SetPause((Time.timeScale > 0));
    }

    private void CheckMovement()
    {
        Debug.Log("Called CheckMovement");
        if (_targetIndex == _movementIndex)
        {
            SetPlayer(true);
        }
        else
        {
            SetPlayer(false);
        }
    }

    private void SetPlayer(bool active) //Enable or disable PlayerMovement component in Player for segment of tutorial that requires input
    {
        Debug.Log($"Called SetPlayer with {active}");
        _playerMove.SetActive(active);
    }

    private void FixedUpdate()
    {
        if (!_atTargetTransform)
        {
            //Update Camera Position and rotation
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, _Positions[_targetIndex].position, _MovementSpeed * Time.deltaTime),
                Quaternion.RotateTowards(transform.rotation, _Positions[_targetIndex].rotation, _RotationSpeed * Time.deltaTime));

            //is the cameara at the target position/orientation?
            if (Vector3.SqrMagnitude(_Positions[_targetIndex].position - transform.position) < 1.0f //SqrDistance is cheaper to calc
                && transform.rotation == _Positions[_targetIndex].rotation) //could be better? might need to be less precise to prevent jitter
            {
                _atTargetTransform = true; //if we are, stop moving the camera, if not, continue
                SetAllBillboardsEnabled(false); //disable all billboards
                _Tooltips[_targetIndex].SetActive(true); //enable the billboard for current tooltip
            }
        }
    }

    //better to do this if using a coroutine
    public void SetCameraIndexPos(int index)
    {
        if(_targetIndex == index) return;
        _targetIndex = index;
        _atTargetTransform = false;
    }

    private void SetAllBillboardsEnabled(bool enabled)
    {
        foreach (GameObject board in _Tooltips)
        {
            board.SetActive(enabled);
        }
    }
}
