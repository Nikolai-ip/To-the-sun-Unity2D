using Player;
using Player.StateMachines;
using UnityEngine;
using StateMachine = AISystem.StateMachine;

public class DetectionController : MonoBehaviour
{
    [SerializeField] private float _detectionDuration;
    [SerializeField] private float _overviewDistance;
    [SerializeField] private float _overviewShadowDistance;
    [SerializeField] private float _overviewAngle;
    [SerializeField] private float _overlapCircleDetection;
    private float _detectionRate;
    private PlayerActor _playerActor;
    private PlayerStateMachine _playerStateMachine;
    private HideController _playerHideController;
    private Transform _transform;
    private StateMachine _enemyStateMachine;
    private Enemy _enemy;
    private bool _canDetect;
    private float DetectionRate
    {
        get => _detectionRate;
        set
        {
            value = Mathf.Clamp(value, 0.0f, _detectionDuration);
            if (!Mathf.Approximately(value, _detectionRate))
            {
                _detectionRate = value;
                if (Mathf.Approximately(_detectionRate, _detectionDuration) && _canDetect)
                {
                    _canDetect = false;
                    GameManager.Instance.ExecuteDeathOnPlayerDetection(_enemy);
                    _enemyStateMachine.PlayerFound();
                }
            }
        }
    }

    private void Start()
    {
        _canDetect = true;
        _enemyStateMachine = GetComponent<StateMachine>();
        _transform = GetComponent<Transform>();
        _playerActor = FindObjectOfType<PlayerActor>();
        _playerHideController = _playerActor.GetComponent<HideController>();
        _playerStateMachine = _playerActor.GetComponent<PlayerStateMachine>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        var distanceToPlayer = Vector2.Distance(_playerActor.transform.position, _transform.position);
        if (CheckPlayerNearby())
        {
            DetectionRate = _detectionDuration;
            return;
        }

        if (IsPlayerVisible())
            DetectionRate += Time.deltaTime *
                             (_overviewDistance / Mathf.Clamp(distanceToPlayer, 0.1f, _overviewDistance));
        else
            DetectionRate -= Time.deltaTime *
                             (_overviewDistance / Mathf.Clamp(distanceToPlayer, 0.1f, _overviewDistance));
    }

    private void OnDrawGizmos()
    {
        var ctg = 1 / Mathf.Tan(_overviewAngle * Mathf.Deg2Rad);
        var cos = Mathf.Cos(_overviewAngle * Mathf.Deg2Rad);
        Gizmos.color = Color.red;
        var transform1 = transform;
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(transform.localScale.x),
                new Vector3(_overviewDistance, _overviewDistance / ctg).y));
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(_overviewDistance * cos * Mathf.Sign(transform.localScale.x),
                new Vector3(_overviewDistance, -_overviewDistance / ctg).y));
        Gizmos.color = new Color(0.5f, 0, 0);
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(_overviewShadowDistance * Mathf.Sign(transform.localScale.x), 0, 0));
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(transform.localScale.x),
                new Vector3(_overviewShadowDistance, _overviewShadowDistance / ctg).y));
        Gizmos.DrawLine(transform.position,
            transform.position + new Vector3(_overviewShadowDistance * cos * Mathf.Sign(transform.localScale.x),
                new Vector3(_overviewShadowDistance, -_overviewShadowDistance / ctg).y));
        Gizmos.DrawWireSphere(transform.position, _overlapCircleDetection);
    }

    private bool IsPlayerVisible()
    {
        if (_playerHideController.IsHidden) return false;

        var distance = _playerHideController.InShadow ? _overviewShadowDistance : _overviewDistance;

        Vector2 playerPosition = _playerActor.transform.position;
        var position = new Vector2(_transform.position.x, _transform.position.y);
        var guardFacing = new Vector2(Mathf.Sign(_transform.localScale.x), 0);
        var dir = playerPosition - position;

        if (Vector2.Distance(playerPosition, position) > distance) return false;
        var angle = Mathf.Acos(Vector2.Dot(guardFacing.normalized, dir.normalized)) * Mathf.Rad2Deg;

        return angle < _overviewAngle && angle > -_overviewAngle;
    }

    private bool CheckPlayerNearby()
    {
        var player = Physics2D.OverlapCircle(_transform.position, _overlapCircleDetection, LayerMask.GetMask("Player"));
        return player && !_playerHideController.IsHidden;
    }
}