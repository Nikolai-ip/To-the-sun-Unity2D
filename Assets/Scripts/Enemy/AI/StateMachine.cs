using Player;
using UnityEngine;

namespace AISystem
{
    public class StateMachine : MonoBehaviour
    {
        [field: SerializeField] public GameObject Weapon { get; private set; }
        private BaseState _currentState;
        public Transform Tr { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public PlayerActor PlayerActor { get; private set; }
        public Enemy Enemy { get; private set; }
        public Animator Animator { get; private set; }
        public Patrolling PatrollingState { get; private set; }
        public Shoot ShootState { get; private set; }
        public ThrowReactionState ThrowReactionState { get; private set; }

        private void Start()
        {
            Enemy = GetComponent<Enemy>();
            Tr = GetComponent<Transform>();
            Rb = GetComponent<Rigidbody2D>();
            PlayerActor = FindObjectOfType<PlayerActor>();
            Animator = GetComponent<Animator>();
            PatrollingState = new Patrolling(this);
            ShootState = new Shoot(this);
            ThrowReactionState = new ThrowReactionState(this);
            ChangeState(PatrollingState);
        }

        private void Update()
        {
            _currentState?.Update();
        }

        private void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        public void ChangeState(BaseState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }

        public void AnimationEventHandler()
        {
            _currentState?.AnimationEventHandler();
        }

        public bool PlayerBehind()
        {
            var dir = PlayerActor.transform.position - Tr.position;
            return (dir.x < 0 && Tr.localScale.x == 1) || (dir.x > 0 && Tr.localScale.x == -1);
        }

        public void HearNoise()
        {
            ChangeState(ThrowReactionState);
        }
    }
}