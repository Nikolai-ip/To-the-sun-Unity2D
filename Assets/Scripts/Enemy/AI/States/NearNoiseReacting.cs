using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AISystem
{
    public class NearNoiseReacting : BaseState
    {
        private int _countOfMoveActions;
        private Vector2 _lastPlayerPos;

        public NearNoiseReacting(StateMachine stateMachine) : base(stateMachine)
        {
            actionChain = new List<StateType> { StateType.Move, StateType.Idle, StateType.Rotate, StateType.Move };
        }

        protected override void ActionChainIsOver()
        {
            stateMachine.ChangeState(stateMachine.PatrollingState);
        }

        public override void Enter()
        {
            _lastPlayerPos = stateMachine.PlayerActor.transform.position;
            if (PlayerIsBehind()) actionChain.Insert(0, StateType.Rotate);
            CurrentState = actionChain.First();
        }

        public override void Update()
        {
            switch (CurrentState)
            {
                case StateType.Move:
                    MoveState(stateMachine.Enemy.NoiseReactingMoveSpeed);
                    break;
                case StateType.Idle:
                    IdleState(stateMachine.Enemy.IdleNoiseReactingDuration);
                    break;
                case StateType.Rotate:
                    RotateState();
                    break;
            }
        }

        protected override Vector2 GetMoveTarget()
        {
            var moveTarget = new Vector2();
            switch (_countOfMoveActions)
            {
                case 0:
                    moveTarget = new Vector2(_lastPlayerPos.x, tr.position.y);
                    break;
                case 1:
                    moveTarget = new Vector2(stateMachine.Enemy.GetClosestPatrollingBorderX, tr.position.y);
                    break;
            }

            _countOfMoveActions++;
            return moveTarget;
        }

        public override void AnimationEventHandler()
        {
            switch (CurrentState)
            {
                case StateType.Rotate:
                    NextState();
                    isRotating = false;
                    if (actionChain.First() == StateType.Rotate)
                    {
                        actionChain.RemoveAt(0);
                        currentActionIndex--;
                    }

                    break;
            }
        }

        public override void Exit()
        {
            _countOfMoveActions = 0;
        }
    }
}