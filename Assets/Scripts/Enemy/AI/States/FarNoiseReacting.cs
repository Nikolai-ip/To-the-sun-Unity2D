using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AISystem
{
    public class FarNoiseReacting : BaseState
    {
        private int _countOfMoveActions;

        public FarNoiseReacting(StateMachine stateMachine) : base(stateMachine)
        {
            actionChain = new List<StateType> { StateType.Move, StateType.Idle, StateType.Rotate, StateType.Move };
        }

        public Vector2? NoiseSourcePos { get; set; }

        protected override void ActionChainIsOver()
        {
            stateMachine.ChangeState(stateMachine.PatrollingState);
        }

        public override void Enter()
        {
            if ( NoiseSourceIsBehind())
                actionChain.Insert(0, StateType.Rotate);
            CurrentState = actionChain.First();
        }

        private bool NoiseSourceIsBehind()
        {
            var dir = NoiseSourcePos - tr.position;
            return (dir.Value.x < 0 && tr.localScale.x == 1) || (dir.Value.x > 0 && tr.localScale.x == -1);
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
                    if (NoiseSourcePos != null)
                    {
                        moveTarget = new Vector2(NoiseSourcePos.Value.x, tr.position.y);
                        Debug.LogWarning("Noise source vector is null");
                    }
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
            NoiseSourcePos = null;
        }
    }
}