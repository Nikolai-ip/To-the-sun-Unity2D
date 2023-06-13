using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    private float _detectionRate = 0;
    [SerializeField] private float _detectionDuration;
    private HideController _playerHideController;
    [SerializeField] private float _overviewDistance;
    [SerializeField] private float _overviewShadowDistance;
    [SerializeField] private float _overviewAngle;
    private Player _player;
    private Transform _transform;
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _transform = GetComponent<Transform>();
        _player = FindObjectOfType<Player>();
        _playerHideController = _player.GetComponent<HideController>();

    }
    private void Update()
    {
        if (IsPlayerVisible())
        {
            _detectionRate += Time.deltaTime;
        }
        else
        {
            _detectionRate -= Time.deltaTime;
        }
        _detectionRate = Mathf.Clamp(_detectionRate, 0.0f, _enemy.DetectionDelay);

    }
    private bool IsPlayerVisible()
    {
        if (_playerHideController.IsHidden) return false;

        float distance = _playerHideController.InShadow ? _overviewShadowDistance : _overviewDistance;

        Vector2 playerPosition = _player.transform.position;
        Vector2 position = new Vector2(_transform.position.x, _transform.position.y);
        Vector2 guardFacing = new Vector2(Mathf.Sign(_transform.localScale.x), 0);
        Vector2 dir = playerPosition - position;

        if (Vector2.Distance(playerPosition, position) > distance) return false;
        float angle = Mathf.Acos(Vector2.Dot(guardFacing.normalized, dir.normalized)) * Mathf.Rad2Deg;

        return angle < _overviewAngle && angle > -_overviewAngle;
    }
    #region "Gizomos"
    private void OnDrawGizmos()
    {
        float ctg = 1 / Mathf.Tan(_overviewAngle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(_overviewAngle * Mathf.Deg2Rad);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewDistance * Mathf.Sign(transform.localScale.x), 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_overviewDistance, _overviewDistance / ctg).y));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_overviewDistance, -_overviewDistance / ctg).y));
        Gizmos.color = new Color(0.5f, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewShadowDistance * Mathf.Sign(transform.localScale.x), 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_overviewShadowDistance, _overviewShadowDistance / ctg).y));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_overviewShadowDistance, -_overviewShadowDistance / ctg).y));

    }
    #endregion
}
