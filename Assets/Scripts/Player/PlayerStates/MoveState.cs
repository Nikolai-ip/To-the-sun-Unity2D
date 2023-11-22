using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Move", menuName = "ScriptableObjects/States/MoveState", order = 1)]
    public class MoveState : HorizontalMoveState
    {

        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "Jump")
            {
                if (sm.CheckPlayerOnGround())
                {
                    sm.ChangeState(sm.Jump,this);
                }
            }
            if (inputAction.name == "MoveXAxis")
            {
                var x = inputAction.ReadValue<Vector2>().x;
                Move(x);
                if (Mathf.Approximately(x, 0)) sm.ChangeState(sm.Idle, this);
            }

            if (inputAction.name == "Interaction")
            {
                sm.InteractionEnvironmentController.EntityInteraction();
            }
        }

        public override void Update()
        {
            if (rb.velocity.y < -2.5f) sm.ChangeState(sm.Fall, this);
        }

        protected override void Move(float x)
        {
            Vector2 targetPosition = tr.position + new Vector3(x * speed, 0);
            Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, smoothTime, maxSpeed);
            rb.velocity = new Vector2(currentVelocity.x, rb.velocity.y);
            Flip(x);
        }

        public override void Enter()
        {
            animator.SetBool("IsWalk", true);
        }

        public override void Exit()
        {
            animator.SetBool("IsWalk", false);
        }
    }
}