using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AISystem;
public class Detector : MonoBehaviour
{
    [SerializeField] StateMachine _stateMachine;
    [Header("Overview")]
    [SerializeField] private float _overviewDistance;
    [SerializeField] private float _overviewShadowDistance;
    [SerializeField] private float _overviewAngle;
    [Header("Detection")]
    private float _detectionRate = 0;
    [SerializeField] private float _detectionDuration;
    private bool IsPlayerVisible()
    {
        if (_stateMachine.PlayerHideController.IsHidden) return false;

        float distance = _stateMachine.PlayerHideController.InShadow ? _overviewShadowDistance : _overviewDistance;

        Vector2 playerPosition = _stateMachine.Player.transform.position;
        Vector2 position = new Vector2(_stateMachine.Tr.position.x, _stateMachine.Tr.position.y);
        Vector2 guardFacing = new Vector2(Mathf.Sign(_stateMachine.Tr.localScale.x), 0);
        Vector2 dir = playerPosition - position;

        if (Vector2.Distance(playerPosition, position) > distance) return false;
        float angle = Mathf.Acos(Vector2.Dot(guardFacing.normalized, dir.normalized)) * Mathf.Rad2Deg;

        return angle < _overviewAngle && angle > -_overviewAngle;
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
        _detectionRate = Mathf.Clamp(_detectionRate, 0.0f, _detectionDuration);
    }
    public virtual void OnDrawGizmos()
    {
        float ctg = 1 / Mathf.Tan(_overviewAngle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(_overviewAngle * Mathf.Deg2Rad);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewDistance * Mathf.Sign(_stateMachine.transform.localScale.x), 0, 0));
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(_stateMachine.transform.localScale.x), new Vector3(_overviewDistance, _overviewDistance / ctg).y));
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(_stateMachine.transform.localScale.x), new Vector3(_overviewDistance, -_overviewDistance / ctg).y));
        Gizmos.color = new Color(0.5f, 0, 0);
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewShadowDistance * Mathf.Sign(_stateMachine.transform.localScale.x), 0, 0));
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(_stateMachine.transform.localScale.x), new Vector3(_overviewShadowDistance, _overviewShadowDistance / ctg).y));
        Gizmos.DrawLine(_stateMachine.transform.position, _stateMachine.transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(_stateMachine.transform.localScale.x), new Vector3(_overviewShadowDistance, -_overviewShadowDistance / ctg).y));
    }
}
