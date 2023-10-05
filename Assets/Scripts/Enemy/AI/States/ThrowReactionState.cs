using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace AISystem
{
    public class ThrowReactionState : BaseState
    {
        private static readonly int Rotate = Animator.StringToHash("Rotate");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");
        private Vector2 _playerOriginPos;

        public ThrowReactionState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override async void Enter()
        {
            _playerOriginPos = stateMachine.PlayerActor.transform.position;
            stateMachine.Animator.SetLayerWeight((int)EnemyAnimatorLayers.WithoutWeapon, 0);
            stateMachine.Animator.SetLayerWeight((int)EnemyAnimatorLayers.IncreaseSpeed, 1);
            if (stateMachine.PlayerBehind()) stateMachine.Animator.SetTrigger(Rotate);
            await Stay();
            var dir = stateMachine.PlayerActor.transform.position - stateMachine.Tr.position;
            stateMachine.StartCoroutine(RunToThrowPosition(Math.Sign(dir.x)));
        }

        private async Task Stay()
        {
            await Task.Delay((int)(stateMachine.Enemy.StandTime * 1000));
        }


        private IEnumerator RunToThrowPosition(float dirX)
        {
            var dir = new Vector2(dirX, 0);

            stateMachine.Animator.SetBool(IsWalk, true);
            stateMachine.Rb.velocity = dir * stateMachine.Enemy.FastWalkVelocity;
            while (Math.Abs(_playerOriginPos.x - stateMachine.Tr.position.x) > 0.1f) yield return null;
            stateMachine.Rb.velocity = Vector2.zero;
            stateMachine.Animator.SetBool(IsWalk, false);
        }

        public override void Exit()
        {
            stateMachine.Animator.SetLayerWeight((int)EnemyAnimatorLayers.WithoutWeapon, 1);
            stateMachine.Animator.SetLayerWeight((int)EnemyAnimatorLayers.IncreaseSpeed, 0);
        }

        public override void AnimationEventHandler()
        {
            stateMachine.Tr.localScale = new Vector3(-stateMachine.Tr.localScale.x, 1);
        }

        public override void CheckTransaction()
        {
        }
    }
}