using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followto;
    [SerializeField] [Range(1, 10)] private float _smoothingCoef;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector2 _minValues;
    [SerializeField] private Vector2 _maxValues;
    private Transform _tr;

    private void Start()
    {
        _tr = GetComponent<Transform>();
    }

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
            -10);

        var smoothedPosition = Vector3.Lerp(_tr.position, boundPosition, _smoothingCoef * Time.fixedDeltaTime);

        _tr.position = smoothedPosition;
    }
}