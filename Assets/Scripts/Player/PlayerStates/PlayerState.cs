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
        protected InputHandler inputHandler;
        public virtual void Initialize(PlayerStateMachine sm)
        {
            this.sm = sm;
            rb = sm.GetComponent<Rigidbody2D>();
            tr = sm.GetComponent<Transform>();
            inputHandler = sm.GetComponent<InputHandler>();
            animator = sm.GetComponent<Animator>();
        }

        public override void Discovered(Enemy enemy)
        {
            sm.EnemyKiller = enemy;
            sm.ChangeState(sm.Die, this);
        }
        
    }
}