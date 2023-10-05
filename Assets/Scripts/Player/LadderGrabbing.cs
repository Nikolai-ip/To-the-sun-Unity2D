using UnityEngine;

namespace Player
{
    public class LadderGrabbing : MonoBehaviour
    {
        [SerializeField] private float _grabingSpeed;
        [SerializeField] private Transform _ladderChecker;
        [SerializeField] private float _checkerRadius;
        [SerializeField] private LayerMask _ladderMask;
        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private bool movingUp;
        public bool OnLadder { get; private set; }

        public bool IsOnLadder
        {
            get => OnLadder;
            private set => OnLadder = value;
        }

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            GetComponent<PlayerMoveController>().OnClimb += ClimbPlayerHandler;
        }

        private void FixedUpdate()
        {
            CheckingLadder();
            ChangeRigidBodyType();
        }

        private void OnDisable()
        {
            GetComponent<PlayerMoveController>().OnClimb -= ClimbPlayerHandler;
        }


        private void CheckingLadder()
        {
            IsOnLadder = Physics2D.OverlapPoint(_ladderChecker.position, _ladderMask);
        }

        private void ChangeRigidBodyType()
        {
            if (IsOnLadder)
                _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            else
                _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void ToggleClimbingStateToLadder(Vector2 inputAxis)
        {
            if (!IsOnLadder)
            {
                _animator.SetLayerWeight((int)PlayerAnimatorLayers.ClimbingOnLadder, 0);
                return;
            }

            movingUp = inputAxis.y > 0;
            if (inputAxis.y == 0) _rigidBody.velocity = Vector2.zero;
            _animator.SetLayerWeight((int)PlayerAnimatorLayers.ClimbingOnLadder, 1);
            _animator.SetFloat("ClimbingOnLadderVelocity", inputAxis.y);
        }

        private void OnLadderClimbAnimationEvent()
        {
            if (_rigidBody.velocity == Vector2.zero)
            {
                var moveY = movingUp ? _grabingSpeed : -_grabingSpeed;
                _rigidBody.velocity = new Vector2(0, moveY);
            }
            else
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }

        private void ClimbPlayerHandler()
        {
            _animator.SetLayerWeight((int)PlayerAnimatorLayers.ClimbingOnLadder, 0);
        }
    }
}