using Player.StateMachines;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerState:State
    {
        protected PlayerStateMachine sm;
        protected Animator animator;
        protected Rigidbody2D rb;
        protected Transform tr;
        public virtual void Initialize(PlayerStateMachine sm)
        {
            this.sm = sm;
            rb = sm.GetComponent<Rigidbody2D>();
            tr = sm.GetComponent<Transform>();
            animator = sm.GetComponent<Animator>();
        }
    }
}