using Player.PlayerStates;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.StateMachines
{
    public class PlayerStateMachine : StateMachine
    {   

        [field: SerializeField] public PlayerState Idle { get; private set; }
        [field: SerializeField] public PlayerState Move { get; private set; }
        [field: SerializeField] public PlayerState Jump { get; private set; }
        [field: SerializeField] public PlayerState LedgeClimbing { get; private set; }
        
        [field: SerializeField] public PlayerState LadderClimbing { get; private set; }
        [field: SerializeField] public PlayerState Fall { get; private set; }
        [field: SerializeField] public PlayerState Die { get; private set; }
        public Collider2D Collider { get; private set; }
        public int GroundLayer { get; private set; }
        public PlayerActor PlayerActor { get; private set; }
        public InteractionEnvironmentController InteractionEnvironmentController { get; private set; }
        protected void Awake()
        {
            Jump.Initialize(this);
            Idle.Initialize(this);
            Move.Initialize(this);
            LedgeClimbing.Initialize(this);
            Fall.Initialize(this);
            LadderClimbing.Initialize(this);
            Die.Initialize(this);
            CurrentState = Idle;
        }

        protected override void Start()
        {
            base.Start();
            currentStateName = "IdleState";
            Collider = GetComponent<Collider2D>();
            GroundLayer = LayerMask.GetMask("Ground");
            PlayerActor = GetComponent<PlayerActor>();
            InteractionEnvironmentController = GetComponent<InteractionEnvironmentController>();
        }
        
        public bool CheckPlayerOnGround()
        {
            var position = transform.position;
            Vector2 topLeftPoint = position;
            var bounds = Collider.bounds;
            topLeftPoint.x -= bounds.extents.x;
            topLeftPoint.y += bounds.extents.y;
            Vector2 bottomRightPoint = position;
            bottomRightPoint.x += bounds.extents.x;
            bottomRightPoint.y -= bounds.extents.y;
            return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, GroundLayer);
        }

        public void OnLedgeFind()
        {
            if (CurrentState)
                CurrentState.LedgeDetected();
        }

        public void OnLadderFind()
        {
            if (CurrentState)
                CurrentState.LadderDetected();
        }

        private void OnDrawGizmos()
        {
            if (!Collider) return;
            var fallState = Fall as FallState;
            Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - Collider.bounds.size.y / 2),
                new Vector3(transform.position.x, transform.position.y - fallState.DistanceTOEndJumpAnimation));
        }


        
    }
}