using System;
using Player.StateMachines;
using UnityEngine;

namespace Player
{
    public class LadderDetector:MonoBehaviour
    {
        
        [SerializeField] private Transform _ladderChecker;
        [SerializeField] private LayerMask _ladderMask;
        [SerializeField] private float _checkerRadius;
        private PlayerStateMachine _playerPlayerStateMachine;
        private bool _enable = true;
        private bool _isOnLadder;

        public bool IsOnLadder
        {
            get => _isOnLadder;
            private set
            {
                if (_enable)
                {
                    if (NewValueIsTrue(value))
                    { 
                        _playerPlayerStateMachine.OnLadderFind();
                    }

                    _isOnLadder = value;
                }
                if (!value)
                {
                    _enable = true;
                }
                
            }
        }

        private bool NewValueIsTrue(bool value) => _isOnLadder != value && value == true;

        private void Start()
        {
            _playerPlayerStateMachine = GetComponent<PlayerStateMachine>();
        }

        private void Update()
        {
            LadderDetection();
        }

        public void DisableLadderDetector()
        {
            _enable = false;
        }
        private void LadderDetection()
        {
            IsOnLadder = Physics2D.OverlapCircle(_ladderChecker.position,_checkerRadius, _ladderMask);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_ladderChecker.position, _checkerRadius);
        }

    }
}