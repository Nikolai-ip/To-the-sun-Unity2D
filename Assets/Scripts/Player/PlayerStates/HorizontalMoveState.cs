using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.PlayerStates
{
    public abstract class HorizontalMoveState:PlayerState
    { 
        [SerializeField] protected float speed;
        [SerializeField] protected float smoothTime; 
        [SerializeField] protected float maxSpeed;
        protected Vector2 currentVelocity = Vector2.zero;
        
        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "MoveXAxis")
            {
                var x = inputAction.ReadValue<Vector2>().x;
                Move(x); 
            }

        }
        protected virtual void Move(float x)
        {
            rb.velocity = new Vector2(x * speed, rb.velocity.y);
            Flip(x);
        }
        
        protected void Flip(float inputHorizontal)
        {
            if (!Mathf.Approximately(inputHorizontal, 0)) tr.localScale = new Vector2(Mathf.Sign(inputHorizontal), 1);
        }
    }
}