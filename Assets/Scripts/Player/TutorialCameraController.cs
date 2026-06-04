using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCameraController : MonoBehaviour
{
    [SerializeField] private List<Transform> _Positions;
    [SerializeField] private float _MovementSpeed = 100f;
    [SerializeField] private float _RotationSpeed = 10f;

    [SerializeField] private bool _atTargetTransform = false;
    [SerializeField] private int _targetIndex;


    private void OnPrevious(InputValue input)
    {
        if (_targetIndex == 0) return;
        _targetIndex--;
        _atTargetTransform = false;
    }

    private void OnNext(InputValue input)
    {
        _targetIndex++;
        if (_targetIndex > _Positions.Count - 1)
        {
            GameplayUITutorial.Instance.TutorialEnd();
            return;
        }
        _atTargetTransform = false;
    }

    private void FixedUpdate()
    {
        if (!_atTargetTransform)
        {
            //Update Camera Position and rotation
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, _Positions[_targetIndex].position, _MovementSpeed * Time.deltaTime),
                Quaternion.RotateTowards(transform.rotation, _Positions[_targetIndex].rotation, _RotationSpeed * Time.deltaTime));

            //check to see if we are at our target position/orientation
            if (Vector3.SqrMagnitude(_Positions[_targetIndex].position - transform.position) < 1.0f //SqrDistance is cheaper to calc
                && transform.rotation == _Positions[_targetIndex].rotation) //could be better? might need to be less precise to prevent errors
            {
                _atTargetTransform = true; //if we are, stop moving the camera, if not, continue
            }
        }
    }

    public void SetCameraIndexPos(int index)
    {
        if(_targetIndex == index) return;
        _targetIndex = index;
        _atTargetTransform = false;
    }
}
