using UnityEngine;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Jump", menuName = "ScriptableObjects/States/JumpState", order = 1)]
    public class JumpState : HorizontalMoveState
    {
        [SerializeField] private float _jumpForce;

        public override void Enter()
        {
            JumpFromGround();
        }

        public override void Update()
        {
            if (rb.velocity.y < 0) sm.ChangeState(sm.Fall, this);
        }

        private void JumpFromGround()
        {
            rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        public override void LedgeDetected()
        {
            sm.ChangeState(sm.LedgeClimbing, this);
        }

        public override void LadderDetected()
        {
            sm.ChangeState(sm.LadderClimbing, this);
        }
    }
}