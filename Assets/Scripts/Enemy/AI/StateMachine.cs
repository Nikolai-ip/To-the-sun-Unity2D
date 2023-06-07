using UnityEngine;

namespace AISystem
{
    public class StateMachine : MonoBehaviour
    {
        public Transform Tr { get; private set; }   
        public Rigidbody2D Rb { get; private set; }
        public Player Player { get; private set; }
        public HideController PlayerHideController { get; private set; }
        private BaseState _currentState;
        public Patrolling PatrollingState;
       
        private void Start()
        {
            Tr = GetComponent<Transform>(); 
            Rb = GetComponent<Rigidbody2D>();
            Player = FindObjectOfType<Player>();
            PlayerHideController = Player.GetComponent<HideController>();
            PatrollingState.Init(this);
            _currentState = PatrollingState;
        }

        public void ChangeState<T>(T state) where T : BaseState
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState.Update();
        }
        private void FixedUpdate()
        {
            if (_currentState)
                _currentState.FixedUpdate();
        }
        #region "Gizomos"
        private void OnDrawGizmos()
        {
            if (_currentState != null)
            {
                _currentState.OnDrawGizmos();
            }

        }
        #endregion
    }
}

