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
    [SerializeField] private float _overlapCircleDetection;
    private Enemy _instance;
    private float DetectionRate
    {
        get
        {
            return _detectionRate;
        }
        set
        {
            value = Mathf.Clamp(value, 0.0f, _detectionDuration);
            if (value != _detectionRate)
            {
                _detectionRate = value;
                if (Mathf.Approximately(_detectionRate,_detectionDuration))
                {
                    GameManager.Instance.StartDeathScene(_instance);   
                }
            }
        }
    }
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _player = FindObjectOfType<Player>();
        _playerHideController = _player.GetComponent<HideController>();
        _instance = GetComponent<Enemy>();
    }
    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(_player.transform.position, _transform.position);
        if (CheckPlayerNearby())
        {
            DetectionRate = _detectionDuration;
            return;
        }
        if (IsPlayerVisible())
        {
            DetectionRate += Time.deltaTime * (_overviewDistance / Mathf.Clamp(distanceToPlayer, 0.1f, _overviewDistance));
        }
        else
        {
            DetectionRate -= Time.deltaTime * (_overviewDistance / Mathf.Clamp(distanceToPlayer, 0.1f, _overviewDistance));
        }
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
    private bool CheckPlayerNearby()
    {
        var player = Physics2D.OverlapCircle(_transform.position, _overlapCircleDetection, LayerMask.GetMask("Player"));
        return player && !_playerHideController.IsHidden;
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
        Gizmos.DrawWireSphere(transform.position, _overlapCircleDetection);
    }
    #endregion
}
