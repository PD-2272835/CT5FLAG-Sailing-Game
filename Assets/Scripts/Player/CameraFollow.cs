using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float speed = 5f;
    private Vector3 _offsetPos;

    private void Awake()
    {
        _offsetPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = _target.position + _offsetPos;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
