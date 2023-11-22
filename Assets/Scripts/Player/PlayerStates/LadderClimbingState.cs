using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "LadderClimbing", menuName = "ScriptableObjects/States/LadderClimbing")]
    public class LadderClimbingState:PlayerState
    {
        private bool _movingUp; 
        [SerializeField] private float _climbingSpeed;
        private float _originGravityScale;
        private LadderDetector _ladderDetector;
        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "MoveYAxis")
            {
                float inputAxisY = inputAction.ReadValue<Vector2>().y;
                if (inputAxisY == 0)
                {
                    rb.velocity = Vector2.zero;
                }
                _movingUp = inputAxisY > 0;
                animator.SetFloat("ClimbingOnLadderVelocity", inputAxisY);
            }

            if (inputAction.name == "Jump")
            {          
                _ladderDetector.DisableLadderDetector();
                sm.ChangeState(sm.Jump,this);
            }

            inputAction = null;
        }

        public override void Enter()
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            animator.SetLayerWeight((int)PlayerAnimatorLayers.ClimbingOnLadder, 1);
            sm.StateMachinesController.SetFirstOrderOfExecution(sm);
        }

        public override void Exit()
        {
            rb.gravityScale = _originGravityScale;
            animator.SetLayerWeight((int)PlayerAnimatorLayers.ClimbingOnLadder, 0);
        }

        public override void AnimationEventHandle()
        {
            if (rb.velocity == Vector2.zero)
            {
                var moveY = _movingUp ? _climbingSpeed : -_climbingSpeed;
                rb.velocity = new Vector2(0, moveY);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        public override void Update()
        {
            if (!_ladderDetector.IsOnLadder || sm.CheckPlayerOnGround())
            {
                rb.velocity = Vector2.zero;
                sm.ChangeState(sm.Idle,this);
            }
        }

        public override void Initialize(PlayerStateMachine sm)
        {
            base.Initialize(sm);
            _originGravityScale = rb.gravityScale;
            _ladderDetector = sm.GetComponent<LadderDetector>();
        }
    }
}