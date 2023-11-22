using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [field: SerializeField] public float FastWalkVelocity { get; private set; }
    [field: SerializeField] public float StandTime { get; private set; }

    [Header("Patrolling")] 
    [SerializeField] private float _leftBorderX;
    

    [SerializeField] private float _rightBorderX;
    [SerializeField] private float _idlePatrollDuration;
    [SerializeField] private float _accelerateSpeed = 0.1f;

    [Header("Shoot")] [SerializeField] private float _delayBeforeShoot;

    [SerializeField] private float _rotateWeaponAngle;
    
    [Header("NoiseReacting")]
    [SerializeField] private float _distanceForNearNoise;

    [SerializeField] private float _distanceForFarNoise;
    [SerializeField] private float _idleNoiseReactingDuration;
    [SerializeField] private float _noiseReactingMoveSpeed;

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.DrawWireSphere(new Vector3(_leftBorderX,position.y), 0.1f);
        Gizmos.DrawWireSphere(new Vector3(_rightBorderX,position.y), 0.1f);

        Gizmos.color = new Color(1, 0.75f, 0);
        Gizmos.DrawLine(new Vector3(position.x,position.y-0.55f), new Vector3(position.x+_distanceForFarNoise,position.y-0.55f));
        Gizmos.DrawLine(new Vector3(position.x,position.y-0.55f), new Vector3(position.x-_distanceForFarNoise,position.y-0.55f));
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(position.x,position.y-0.55f), new Vector3(position.x+_distanceForNearNoise,position.y-0.55f));
        Gizmos.DrawLine(new Vector3(position.x,position.y-0.55f), new Vector3(position.x-_distanceForNearNoise,position.y-0.55f));
    }

    #region "Properties"

    public float MoveVelocity => _moveVelocity;
    public float LeftBorder => _leftBorderX;
    public float RightBorder => _rightBorderX;
    public float IdlePatrollDuration => _idlePatrollDuration;
    public float AccelerateSpeed => _accelerateSpeed;
    public float DelayBeforeShoot => _delayBeforeShoot;

    public float RotateWeaponAngle => _rotateWeaponAngle;
    public float DistanceForNearNoise => _distanceForNearNoise;
    public float DistanceForFarNoise => _distanceForFarNoise;
    public float IdleNoiseReactingDuration => _idleNoiseReactingDuration;
    public float NoiseReactingMoveSpeed => _noiseReactingMoveSpeed;
    public float GetClosestPatrollingBorderX
    {
        get
        {
            var position = transform.position;
            float closestBorderX =
                Math.Abs(position.x - _leftBorderX) > Math.Abs(position.x - _rightBorderX)
                    ? _rightBorderX
                    : _leftBorderX;
            return closestBorderX;
        }
    }

    #endregion
}