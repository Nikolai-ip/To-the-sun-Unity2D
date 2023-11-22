using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class Patrolling : BaseState
    {
        public Patrolling(StateMachine stateMachine) : base(stateMachine)
        {
            actionChain = new List<StateType> { StateType.Move, StateType.Idle, StateType.Rotate };
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            switch (CurrentState)
            {
                case StateType.Move:
                    MoveState(stateMachine.Enemy.MoveVelocity);
                    break;
                case StateType.Idle:
                    IdleState(stateMachine.Enemy.IdlePatrollDuration);
                    break;
                case StateType.Rotate:
                    RotateState();
                    break;
            }
        }


        private Vector2 GetFarthestTarget()
        {
            var position = tr.position;
            var leftPatrolTarget = new Vector2(stateMachine.Enemy.LeftBorder, position.y);
            var rightPatrolTarget = new Vector2(stateMachine.Enemy.RightBorder, position.y);
            var distanceToLeftTarget = Vector2.Distance(position, leftPatrolTarget);
            var distanceToRightTarget = Vector2.Distance(position, rightPatrolTarget);
            var farthestTarget = distanceToLeftTarget > distanceToRightTarget ? leftPatrolTarget : rightPatrolTarget;
            return farthestTarget;
        }

        public override void AnimationEventHandler()
        {
            switch (CurrentState)
            {
                case StateType.Rotate:
                    NextState();
                    isRotating = false;
                    break;
            }
        }

        protected override Vector2 GetMoveTarget()
        {
            return GetFarthestTarget();
        }

        public override void Exit()
        {
            base.Exit();
            animator.SetBool("IsWalk", false);
        }
    }
}