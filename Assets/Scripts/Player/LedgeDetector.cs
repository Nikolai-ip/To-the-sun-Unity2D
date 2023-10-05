using Player.StateMachines;
using UnityEngine;

namespace Player
{
    public class LedgeDetector : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _ground;
        private PlayerStateMachine _playerPlayerStateMachine;
        private Transform _tr;
        public bool CanDetected { get; set; }
        public Vector2 Position { get; private set; }

        private void Start()
        {
            _tr = GetComponent<Transform>();
            _playerPlayerStateMachine = GetComponentInParent<PlayerStateMachine>();
        }

        private void Update()
        {
            Position = _tr.position;
            if (CanDetected)
            {
                var climb =  Physics2D.OverlapCircle(_tr.position, _radius, _ground);
                if (climb)
                {
                    _playerPlayerStateMachine.OnLedgeFind();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) CanDetected = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) CanDetected = true;
        }
    }
}