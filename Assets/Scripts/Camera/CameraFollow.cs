using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followto;
    [SerializeField] [Range(1, 10)] private float _smoothingCoef;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _minValues;
    [SerializeField] private Vector3 _maxValues;


    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        var followToPosition = _followto.position + _offset;

        var boundPosition = new Vector3(
            Mathf.Clamp(followToPosition.x, _minValues.x, _maxValues.x),
            Mathf.Clamp(followToPosition.y, _minValues.y, _maxValues.y),
            Mathf.Clamp(followToPosition.z, _minValues.z, _maxValues.z));

        var smoothedPosition = Vector3.Lerp(transform.position, boundPosition, _smoothingCoef * Time.fixedDeltaTime);

        transform.position = smoothedPosition;
    }
}