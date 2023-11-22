using Player.StateMachines;
using UnityEngine;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Fall", menuName = "ScriptableObjects/States/FallState", order = 1)]
    public class FallState:HorizontalMoveState
    {
        [SerializeField] private int _maxFallSpeed;
        [SerializeField] private float _gravityFallScale;
        [SerializeField] private float _distanceToEndJumpAnimation;
        [SerializeField] private float _deadlyFallTime;
        private float _elapsedTime;
        private float _originalGravityScale;
        public float DistanceTOEndJumpAnimation => _distanceToEndJumpAnimation;
        public override void Initialize(PlayerStateMachine sm)
        {
            base.Initialize(sm);
            _originalGravityScale = rb.gravityScale;
        }
        
        public override void Enter()
        {
            animator.SetBool("IsFalling", true);
            rb.gravityScale = _originalGravityScale * _gravityFallScale;
        }

        public override void Exit()
        {
            rb.gravityScale = _originalGravityScale;
        }

        public override void Update()
        {
            LimitMaxFallVelocity();
            CalculateFallTimeToDeath();
            Vector2 footPoint = new Vector3(tr.position.x, tr.position.y - sm.Collider.bounds.size.y / 2);
            var ground = Physics2D.Raycast(footPoint, Vector2.down, _distanceToEndJumpAnimation, sm.GroundLayer);
            if (ground)
            {
                    animator.SetBool("IsFalling", false);
            }
            
            if (Mathf.Approximately(rb.velocity.y, 0)) sm.ChangeState(sm.Idle, this);
        }
        
        private void LimitMaxFallVelocity()
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -_maxFallSpeed));
        }

        private void CalculateFallTimeToDeath()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _deadlyFallTime)
            {
                
            }
        }
        public override void LadderDetected()
        {
            sm.ChangeState(sm.LadderClimbing, this);
        }
    }
}