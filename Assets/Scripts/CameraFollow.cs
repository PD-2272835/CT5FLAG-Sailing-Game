using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] public float speed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private void Start()
    {
        transform.position = _target.position + offset;
    }
    void FixedUpdate()
    {
        Vector3 targetPosition = _target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
