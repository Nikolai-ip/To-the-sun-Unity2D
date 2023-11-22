using System.Collections;
using Player.StateMachines;
using UnityEngine;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Ledge", menuName = "ScriptableObjects/States/LedgeState", order = 1)]
    public class LedgeClimbingState : PlayerState
    {      
        [SerializeField]  private Vector2 offset1;
        [SerializeField] private Vector2 offset2;
        [SerializeField] private float _moveToClimbDuration;
        private LedgeDetector _ledgeDetector;
        private Vector2 climbBeginPos;
        private Vector2 climbEndPos;
        private float _originGravityScale;

        public override void Initialize(PlayerStateMachine sm)
        {
            base.Initialize(sm);
            _ledgeDetector = sm.GetComponentInChildren<LedgeDetector>();
            _originGravityScale = rb.gravityScale;
        }

        public override void Enter()
        {
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            LedgeCLimbStart();
        }

        private void LedgeCLimbStart()
        {
            var ledgePos = _ledgeDetector.Position;
            climbBeginPos = ledgePos + new Vector2(offset1.x * Mathf.Sign(tr.localScale.x), offset1.y);
            climbEndPos = ledgePos + new Vector2(offset2.x * Mathf.Sign(tr.localScale.x), offset2.y);
            animator.SetTrigger("Climb");
            _ledgeDetector.CanDetected = false;
            sm.Collider.enabled = false;
            
            sm.StartCoroutine(MoveObjectToPosition(tr, climbBeginPos, _moveToClimbDuration));
        }

        private IEnumerator MoveObjectToPosition(Transform transform, Vector3 targetPosition, float duration)
        {
            var timeElapsed = 0f;
            var startingPosition = transform.position;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                var t = Mathf.Clamp01(timeElapsed / duration);
                transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
                yield return null;
            }

            transform.position = targetPosition;
        }

        public override void AnimationEventHandle()
        {
            sm.ChangeState(sm.Idle,this);
        }

        public override void Exit()
        {
            LedgeClimbOver();
        }

        void LedgeClimbOver()
        {
            rb.gravityScale = _originGravityScale;
            rb.bodyType = RigidbodyType2D.Dynamic;
            tr.position = climbEndPos;
            _ledgeDetector.CanDetected = true;
            sm.Collider.enabled = true;
        }
        
    }
}