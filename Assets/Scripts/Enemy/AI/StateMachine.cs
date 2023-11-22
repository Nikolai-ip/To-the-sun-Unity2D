using Player;
using UnityEngine;

namespace AISystem
{
    public class StateMachine : MonoBehaviour
    {
        [field: SerializeField] public GameObject Weapon { get; private set; }
        [SerializeField] private string _currentStateName;
        [SerializeField] private StateType _startState;
        private BaseState _currentState;
        public Transform Tr { get; private set; }
        public PlayerActor PlayerActor { get; private set; }
        public Enemy Enemy { get; private set; }
        public Animator Animator { get; private set; }
        public Patrolling PatrollingState { get; set; }
        public BaseState ShotState { get; set; }
        public NearNoiseReacting NearNoiseReacting { get; private set; }
        public FarNoiseReacting FarNoiseReacting { get; private set; }
        private BaseState IdleState { get; set; }

        private void Start()
        {
            Enemy = GetComponent<Enemy>();
            Tr = GetComponent<Transform>();
            PlayerActor = FindObjectOfType<PlayerActor>();
            Animator = GetComponent<Animator>();
            PatrollingState = new Patrolling(this);
            ShotState = new Shot(this);
            NearNoiseReacting = new NearNoiseReacting(this);
            FarNoiseReacting = new FarNoiseReacting(this);
            IdleState = new Idle(this);
            SetStartState();
        }

        private void Update()
        {
            _currentState.Update();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        private void SetStartState()
        {
            switch (_startState)
            {
                case StateType.Idle:
                    _currentState = IdleState;
                    break;
                case StateType.Patrolling:
                    _currentState = PatrollingState;
                    break;
                default:
                    _currentState = IdleState;
                    break;
            }

            _currentStateName = _currentState.Name;
        }

        public void ChangeState(BaseState state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
            _currentStateName = _currentState.Name;
        }

        public void AnimationEventHandler()
        {
            _currentState.AnimationEventHandler();
        }

        public void HearNoiseFromItem(Vector2 noiseSourcePos)
        {
            _currentState.HearNoise(noiseSourcePos);
        }

        public void PlayerFound()
        {
            ChangeState(ShotState);
        }

        public void Fire()
        {
            if (_currentState is Shot shotState) shotState.FireAShot();
        }
    }
}