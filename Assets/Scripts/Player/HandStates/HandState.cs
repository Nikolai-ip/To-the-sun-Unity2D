using Player.PlayerStates;
using Player.StateMachines;
using UnityEngine;

namespace Player.HandStates
{
    public class HandState:State
    {
        protected HandStateMachine sm;
        protected Animator animator;
        protected Rigidbody2D rb;
        protected Transform tr;

        public virtual void Initialize(HandStateMachine sm)
        {
            this.sm = sm;
            rb = sm.GetComponent<Rigidbody2D>();
            tr = sm.GetComponent<Transform>();
            animator = sm.GetComponent<Animator>();
        }

        public override void Discovered(Enemy enemy)
        {
            sm.ChangeState(sm.Null,this);
        }
    }
}