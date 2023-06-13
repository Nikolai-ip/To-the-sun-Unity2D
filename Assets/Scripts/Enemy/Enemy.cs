using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [Header("Patrolling")]
    [SerializeField] private Vector2 _leftBorder;
    [SerializeField] private Vector2 _rightBorder;
    [SerializeField] private float _idlePatrollDuration;
    [SerializeField] private float _accelerateSpeed = 0.1f;
    [Header("Space")]
    [SerializeField] private float _detectionDelay;

    #region "Properties"
    public float MoveVelocity => _moveVelocity;
    public float DetectionDelay => _detectionDelay;
    public Vector2 LeftBorder => _leftBorder;
    public Vector2 RightBorder => _rightBorder;
    public float IdlePatrollDuration => _idlePatrollDuration;
    public float AccelerateSpeed => _accelerateSpeed;   
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_leftBorder, 0.1f);
        Gizmos.DrawWireSphere(_rightBorder, 0.1f);

    }
}
